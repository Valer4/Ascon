using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RestService
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // Издатель токена.
        public const string AUDIENCE = "MyAuthClient"; // Потребитель токена.
        private const string KEY = "mysupersecret_secretkey!123";   // Ключ для шифрации.
        public const int LIFETIME = 1; // Время жизни токена - 1 минута.
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
