using System.Threading.Tasks;
using buckstore.auth.service.domain.SeedWork;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUserByEmail(string email);
    }
}