using buckstore.auth.service.environment.Configuration;
using buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook.Contracts;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook
{
    public class FacebookIdendity : IFacebookIdendity
    {
        private readonly FacebookSettings _facebbokSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookIdendity(FacebookSettings facebbokSettings, IHttpClientFactory httpClientFactory)
        {
            _facebbokSettings = facebbokSettings;
            _httpClientFactory = httpClientFactory;
        }

        public Task<FacebookUserInfo> GetUserInfo(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<FacebookTokenValidationResult> ValidateAccessToken(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
