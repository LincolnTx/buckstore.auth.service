using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.environment.Configuration;
using buckstore.auth.service.infrastructure.CrossCutting.identity.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.JwtIdentity
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(JwtSettings jwtSettings, ILogger<IdentityService> logger)
        {
            _jwtSettings = jwtSettings;
            _logger = logger;
        }

         public AuthenticationResult GenerateToken(User user)
         {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id.ToString()),
            };

            claims.Add(new Claim("Role", UserType.From(user.UserType).Name));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.TokenIssuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id.ToString(),
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
         }

        public async Task<AuthenticationResult> RefreshToken(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = _jwtSettings.TokenIssuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            return tokenValid.Claims;
        }

        public bool ValidateToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = _jwtSettings.TokenIssuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var jwtSecutiryTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                jwtSecutiryTokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao validar token jwt {0}", e);
                return false;
            }
        }
    }
}