using Laborator54522021.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Licenta.Utilities.JWTutils
{
    public class JWTUtils:IJWTUtils
    {
        private readonly AppSettings _appSettings;
        public JWTUtils(IOptions<AppSettings> appSettings )
        { 
            _appSettings = appSettings.Value;
        }
        public string GenerateJWTToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString())}), // vrem ca din token sa putem scoate id-ul
                Expires= DateTime.UtcNow.AddDays(10), //data de expirare
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(appPrivateKey), SecurityAlgorithms.HmacSha256Signature) // algoritmul de encriptie 

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
                  
        }
        public Guid ValidateJWTToken(string token)
        {
            if (token == null)
                return Guid.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);

            var tokenValidationParameters = new TokenValidationParameters
            { 
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=new SymmetricSecurityKey(appPrivateKey),
                ValidateIssuer=false,
                ValidateAudience=false,
                ClockSkew=TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validateToken);
                var jwtToken = (JwtSecurityToken)validateToken; // am facut conversia pentru a putea extrage id-ul
                var userId = new Guid(jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value);
                return userId;
            }

            catch(Exception) 
            {
                return Guid.Empty;
            }
        }
    }
} 
