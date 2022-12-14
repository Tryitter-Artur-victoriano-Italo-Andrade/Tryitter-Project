using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tryitter_Project.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Auth.Constants;

namespace Tryitter_Project.Services;

public class TokenGenerator
{

  public string Generate(Student student)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = AddClaims(student),

      SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)),
            SecurityAlgorithms.HmacSha256Signature
        ),
      Expires = DateTime.Now.AddDays(1)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
  private ClaimsIdentity AddClaims(Student student)
  {
    ClaimsIdentity identityStudent = new();
    identityStudent.AddClaim(new Claim("StudentId", student.StudentId.ToString()));

    return identityStudent;
  }
}