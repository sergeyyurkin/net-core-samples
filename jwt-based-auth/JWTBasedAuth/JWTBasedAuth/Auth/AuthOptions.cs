using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWTBasedAuth.Auth
{
    public class AuthOptions
    {
        private const string KEY = "mysupersecret_secretkey!123";   
        
        /// <summary>
        /// Издатель токена.
        /// </summary>
        public const string ISSUER = "MyAuthServer"; 

        /// <summary>
        /// Потребитель токена.
        /// </summary>
        public const string AUDIENCE = "MyAuthClient"; 
        
        /// <summary>
        /// Время жизни токена (мин).
        /// </summary>
        public const int LIFETIME = 1; 

        /// <summary>
        /// Возвращает ключ шифрования.
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
