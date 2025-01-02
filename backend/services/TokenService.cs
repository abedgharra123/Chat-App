using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.interfaces;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.services;

public class TokenService(IConfiguration config) : ITokenService
{
    string ITokenService.CreateToken(User user)
    {
        var keyToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"] ?? throw new ArgumentException("Token key not found")));
        var credentials = new SigningCredentials(keyToken, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new(ClaimTypes.Name, user.Username)
            ]),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
