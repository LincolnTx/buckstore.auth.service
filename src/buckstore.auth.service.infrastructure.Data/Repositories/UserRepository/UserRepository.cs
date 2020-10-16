using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.infrastructure.Data.Context;

namespace buckstore.auth.service.infrastructure.Data.Repositories.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}