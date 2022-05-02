using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestService.Controllers.ConcreteDefinitions
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AbstractController
    {
        // Тестовые данные вместо использования базы данных.
        private List<UserEntity> users = new()
        {
            new UserEntity { UserName = "UserNameX", Password = "PasswordX", Roles = new List<RoleEntity> { new RoleEntity { Name = "admin" } } }
        };

        [HttpPost("token")]
        public IActionResult GetAuthorizationToken()
        {
            string json = GetPostBody();
            if (string.IsNullOrEmpty(json))
                return BadRequest(string.Empty); // Нужно придумать описание ошибки.

            var pattern = new {
                userName = string.Empty,
                password = string.Empty };
            var paramsObj = JsonConvert.DeserializeAnonymousType(json, pattern);

            ClaimsIdentity? identity = GetIdentity(paramsObj?.userName, paramsObj?.password);
            if (null == identity)
                return BadRequest(new { errorText = "Invalid username or password." });

            DateTime now = DateTime.UtcNow;

            // Создаем JWT-токен.
            JwtSecurityToken jwt = new(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new {
                accessToken = encodedJwt,
                userName = identity.Name };
            return Json(response);
        }

        private ClaimsIdentity? GetIdentity(string? userName, string? password)
        {
            UserEntity? user = users.FirstOrDefault(x => x.UserName == userName && x.Password == password);

            if (null != user && user.Roles.Any())
            {
                List<Claim> claims = new()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Roles[0].Name)
                };
                ClaimsIdentity claimsIdentity = new(
                    claims, "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // Если пользователь не найден.
            return null;
        }
    }
}
