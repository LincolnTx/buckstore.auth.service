using System;

namespace buckstore.auth.service.environment.Configuration
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
        public string TokenIssuer { get; set; }
        public string Audience { get; set; }
    }
}