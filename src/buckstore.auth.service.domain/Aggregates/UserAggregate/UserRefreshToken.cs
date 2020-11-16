using buckstore.auth.service.domain.SeedWork;
using System;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public class UserRefreshToken : Entity
    {
        private string _refreshToken;

        public UserRefreshToken(string refreshToken)
        {
            _refreshToken = refreshToken;
        }
        protected UserRefreshToken() { }

        public void UpdateRefreshToken(string newRefreshToken)
        {
            _refreshToken = newRefreshToken;
        }
    }
}
