using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StudentServiceAPI.Auth
{
    internal class AuthTokenCheckerOptions
    {
        public string ISSUER { get; set; } // = "MyAuthServer"; // издатель токена
        public string AUDIENCE { get; set; } // = "http://localhost:51884/"; // потребитель токена
        public string KEY { get; set; } // = "mysupersecret_secretkey!123";   // ключ для шифрации
        public int LIFETIMEACCESSMIN { get; set; } //= 1; // время жизни токена - 1 минута

        public AuthTokenCheckerOptions()
        {

        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
