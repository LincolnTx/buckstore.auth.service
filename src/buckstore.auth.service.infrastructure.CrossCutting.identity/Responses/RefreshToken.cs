using System;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.Responses
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public string UserId { get; set; }

        public RefreshToken()
        {
            Token = Guid.NewGuid().ToString();
        }
    }
}