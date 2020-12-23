using buckstore.auth.service.environment.Configuration;
using buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook.Contracts;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook
{
    public class FacebookIdendity : IFacebookIdendity
    {
        private readonly FacebookSettings _facebookSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<FacebookIdendity> _logger;

        public FacebookIdendity(FacebookSettings facebbokSettings, HttpClient httpClient, ILogger<FacebookIdendity> logger )
        {
            _facebookSettings = facebbokSettings;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<FacebookUserInfo> GetUserInfo(string accessToken)
        {
             var formattedUrl = string.Format(_facebookSettings.UserInfoUrl, accessToken);

            try
            {
                var responseAsString = await RealizeGetHttpRequest(formattedUrl);
                return JsonConvert.DeserializeObject<FacebookUserInfo>(responseAsString);
            }
            catch (HttpRequestException httpException)
            {
                _logger.LogError("Erro ao buscar informações do usuário do facebook", e.Message);
                throw httpException;
            }
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessToken(string accessToken)
        {
            var formattedUrl = string.Format(_facebookSettings.ValidateTokenUrl, accessToken, _facebookSettings.AppId, 
                _facebookSettings.AppSecret);

            try
            {
                var responseAsString = await RealizeGetHttpRequest(formattedUrl);
                return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
            }
            catch (HttpRequestException httpException )
            {
                _logger.LogError("Erro ao realizar confirmação de token do Facebook Auth", e.Message);
                throw httpException;
            }
        }

        async Task<string> RealizeGetHttpRequest(string requestUrl )
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
