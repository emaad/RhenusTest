using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace RehnusTestWebAPI.Helpers
{
    /// <summary>
    /// This class will be used to generate and authenticate token
    /// </summary>
    public class JWTAuthorization
    {
        private readonly IConfiguration configuration;

        public JWTAuthorization(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /// <summary>
        /// This method will generate new token for a current user.
        /// </summary>
        /// <param name="userId">current user id will be used to create token</param>
        /// <returns></returns>
        public string GenerateToken(string userId)
        {
            var mySecret = configuration["Jwt:Key"];//Getting value from the app settings.
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = configuration["Jwt:Issuer"];//Getting value from the app settings.
            var myAudience = configuration["Jwt:Issuer"];//Getting value from the app settings.

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor//Defining token claims values
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)//This will encrypt the value
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);//Returning the newly generated token.
        }
        public string ValidateToken(HttpRequest request)
        {
            string Token = request.Headers["Authorization"];
            string[] values = Token.Split("Bearer ");//As header have authorization section and it contain Bearer so we spliting it to get Token and validate it.

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = new JwtSecurityToken();
            if (values.Length > 1)//just in case if someone dont provide bearer with token.
            {
                token = handler.ReadJwtToken(values[1]);
            }
            else
            {
                token = handler.ReadJwtToken(values[0]);
            }
            var parameters = token.Payload;

            return parameters["nameid"].ToString();//returning token claims
        }
    }
}
