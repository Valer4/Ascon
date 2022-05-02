using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace WcfService.Security.Authorization
{
    internal class Validator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("userName");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password");

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 != comparer.Compare(userName, "UserNameX"))
                throw new SecurityTokenValidationException("Указанный логин неверный.");

            if (0 != comparer.Compare(password, "PasswordX"))
                throw new SecurityTokenValidationException("Указанный пароль неверный.");
        }
    }
}
