using buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook.Contracts;
using System.Threading.Tasks;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook
{
    public interface IFacebookIdendity
    {
        Task<FacebookTokenValidationResult> ValidateAccessToken(string accessToken);
        Task<FacebookUserInfo> GetUserInfo(string accessToken);
    }
}
