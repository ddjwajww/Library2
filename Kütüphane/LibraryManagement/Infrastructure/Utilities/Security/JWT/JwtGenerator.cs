using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities.Security.JWT
{
    public class JwtGenerator
    {
        private readonly IConfiguration _config;
        private TokenOptions _tokenOptions;
        private DateTime _expirationDate;

        public JwtGenerator(IConfiguration config)
        {
            _config = config;
            _tokenOptions = _config.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken GenerateAccessToken()
        {
            _expirationDate = DateTime.Now.AddMinutes(_tokenOptions.Expiration);

            var sKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var sCredentials = new SigningCredentials(sKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken
                (
                    issuer: _tokenOptions.Issuer,
                    audience: _tokenOptions.Audience,
                    expires: _expirationDate,
                    notBefore: DateTime.Now,
                    claims: new List<Claim> { new Claim("UserId", "VALUE1"), new Claim("KEY2", "VALUE2"), new Claim("KEY3", "VALUE3") },
                    signingCredentials: sCredentials
                );

            var jwtHandler = new JwtSecurityTokenHandler();

            var token = jwtHandler.WriteToken(securityToken);
            

            return new AccessToken()
            {
                Token = token,
                ExpirationDate = _expirationDate,
                Claims = null
            };

            
        }
    }
}
