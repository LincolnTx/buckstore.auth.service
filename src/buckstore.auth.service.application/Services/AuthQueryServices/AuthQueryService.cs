using System.Threading.Tasks;
using buckstore.auth.service.environment.Configuration;
using MongoDB.Driver;

namespace buckstore.auth.service.application.Services.AuthQueryServices
{
    public class AuthQueryService : IAuthQueryService
    {
        private readonly IMongoCollection<UserQueryModel> _authCollection;
        private readonly AppConfigurations _appConfigurations;

        public AuthQueryService(AppConfigurations appConfigurations)
        {
            _appConfigurations = appConfigurations;
            var mongoUrl = new MongoUrl(_appConfigurations.MongoDbConfiguration.ConnectionString);
            var client = new MongoClient(mongoUrl);
            var database = client.GetDatabase(_appConfigurations.MongoDbConfiguration.DatabaseName);
            
            _authCollection = database.GetCollection<UserQueryModel>(_appConfigurations.MongoDbConfiguration.CollectionName);
        }

        public async Task<UserQueryModel> GetByEmail(string email)
        {
            var user = await _authCollection.FindAsync<UserQueryModel>(user => user.Email == email);

            return user.FirstOrDefault();
        }

        public async Task<UserQueryModel> Create(UserQueryModel user)
        {
           await _authCollection.InsertOneAsync(user);
           return user;
        }

        public UserQueryModel Update(string email, UserQueryModel user)
        {
            var updatedUser = _authCollection.ReplaceOne(oldUser => oldUser.Email == email, user);
            
            return !updatedUser.IsAcknowledged ? null : user;
        }

        public bool Delete(string email)
        {
            var deletedUser = _authCollection.DeleteOne(user => user.Email == email);
            return deletedUser.IsAcknowledged;
        }
    }
}