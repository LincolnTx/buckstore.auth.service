using buckstore.auth.service.domain.SeedWork;
using System;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public class UserRefreshToken : Entity
    {
        private string _refreshToken;

        public UserRefreshToken(string refreshToken)
        {
            SetId();
            _refreshToken = refreshToken;
        }
        protected UserRefreshToken() { }

        public UserRefreshToken UpdateRefreshToken(string newRefreshToken)
        {
            _refreshToken = newRefreshToken;
            return this;
        }
    }
}
