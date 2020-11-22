using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.infrastructure.CrossCutting.identity.Responses;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.JwtIdentity
{
    public interface IIdentityService
    {
        AuthenticationResult GenerateToken(User user);
        bool ValidateToken(string token);
        Task<AuthenticationResult> RefreshToken(string token, string refreshToken);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}