using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.api.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // create some claims

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            // check if any roles

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //reading the key from configuration

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));

            // creating securing sing in credentials

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //creating toke

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
