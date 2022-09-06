using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Jwt
{
    public class JwtHelper
    {
        public static string GetJwtToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Username));
            claims.Add(new Claim(ClaimTypes.Role, UserRole.User.ToString()));
            claims.Add(new Claim("userId", user.Id.ToString()));
            
            if(user.UserRole == UserRole.Administrator)
                claims.Add(new Claim(ClaimTypes.Role, UserRole.Administrator.ToString()));
            
            var jwtSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("__AndreiJWTKey99__"));
            var credentials = new SigningCredentials(jwtSecret, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(7),
                claims: claims,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}