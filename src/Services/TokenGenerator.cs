using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tryitter_Project.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Auth.Constants;

namespace LifeBankAuth.Services;

    public class TokenGenerator
    {

        public string Generate(Student student)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Expires = DateTime.Now.AddDays(1)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }