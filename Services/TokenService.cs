using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Intefaces;
using MyWebApi.Models;

namespace MyWebApi.Services
{
    public class TokenService : ITokenService
    {
        public readonly IConfiguration _config;
        public readonly SymmetricSecurityKey _Key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public string? CreateToken(AppUser user)
        {
            var claims  = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName , user.UserName),         
                new Claim(JwtRegisteredClaimNames.Aud, "http://localhost:5246"),

            } ;

            var creds = new SigningCredentials(_Key , SecurityAlgorithms.HmacSha512Signature);

            var tokenDescribtor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audince"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();


            var token = tokenHandler.CreateToken(tokenDescribtor);

            return tokenHandler.WriteToken(token);
        }
    }
}