using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace WebApi.Services
{
    public class JsonWebTokenHelper
    {
        private static readonly SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hj32ex?486sgi0p+d65g"));//对称秘钥
        public static string CreateToken(string name,string role)
        {
            var claim = new Claim[]{
                    new Claim(ClaimTypes.Name,name),
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.DateOfBirth,DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//签名证书(秘钥，加密算法)
            JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: "jwtDrSim",
            audience: "jwtDrSim",
            claims: claim,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
            return tokenString;
        }

        public static (bool validToken, IIdentity Identity) ValidateToken(string tokenString)
        {
            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "jwtDrSim",
                ValidAudience = "jwtDrSim",
                IssuerSigningKey = key,
            };
            try
            {
                ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(tokenString, tokenValidationParams, out SecurityToken securityToken);
                var claims = claimsPrincipal.Claims.ToList();
                long notBefore = long.Parse(claims.Where(i => i.Type == "nbf").First().Value);
                long expires = long.Parse(claims.Where(i => i.Type == "exp").First().Value);
                long utcNow = EpochTime.GetIntDate(DateTime.UtcNow.ToUniversalTime());
                
                if (expires - utcNow < 0)
                {
                    return (false,null);
                }
                return (true, claimsPrincipal.Identity);
            }
            catch
            {
                return (false, null);
            }
        }

    }
}