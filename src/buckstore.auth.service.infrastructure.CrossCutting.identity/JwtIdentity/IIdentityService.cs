using System.Threading.Tasks;
using buckstore.auth.service.infrastructure.CrossCutting.identity.Responses;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.JwtIdentity
{
    public interface IIdentityService
    {
        AuthenticationResult GenerateToken(string userId, string email);
        Task<AuthenticationResult> RefreshToken(string token, string refreshToken);
    }
}