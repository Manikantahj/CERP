using CERP.ModelDataTransferObjects.Users;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CERP.Token
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration configuration;
        public TokenGeneratorService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateToken(string user_name, string password)
        {
            //=== app setting json inda jwt key tagondu secretkey alli store madtha idini
            var secretkey = configuration["Jwt:Key"]!;


            //=====================JWT security key object create maadi secret key naa endcoding
            //======madkollutee string alli irodanna (secretkey) byte format ge UTF use madkondu convert madatte and store madatte
            //==UTF-8 = one type of encoding
            //=====Unicode Transformation Format – 8 bit
            var securitykey =new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var credential = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity([
                    new Claim(ClaimTypes.Name,user_name),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim("user-function","123")
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Jwt:DurationInMinutes"])),
                SigningCredentials = credential,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}
