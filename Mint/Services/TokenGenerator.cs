using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using Mint.Models;

namespace Mint.Services;

public class TokenGenerator
{
  public string Generate(User user)
  {
    var Secret = "7f43a19bbde92e1f5873ac4593f3745a";

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = AddClaims(user),
      Expires = DateTime.UtcNow.AddHours(24),
      SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)),
        SecurityAlgorithms.HmacSha256Signature
      )
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  private ClaimsIdentity AddClaims(User user)
  {
    ClaimsIdentity claimsIdentity = new();

    claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
    claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "User"));

    return claimsIdentity;
  }
}