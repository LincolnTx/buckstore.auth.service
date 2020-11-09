using System.Threading.Tasks;

namespace buckstore.auth.service.application.Services.AuthQueryServices
{
    public interface IAuthQueryService
    {
        Task<UserQueryModel> GetByEmail(string email);
        Task<UserQueryModel>  Create(UserQueryModel user);
        UserQueryModel Update(string email, UserQueryModel user);
        bool Delete(string email);
    }
}