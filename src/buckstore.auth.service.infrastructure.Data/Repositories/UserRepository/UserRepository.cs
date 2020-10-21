using System.Threading.Tasks;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace buckstore.auth.service.infrastructure.Data.Repositories.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(user => user._email == email);
        }
    }
}