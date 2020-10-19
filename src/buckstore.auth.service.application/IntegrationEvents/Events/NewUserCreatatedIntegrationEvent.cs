
namespace buckstore.auth.service.application.IntegrationEvents.Events
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public string Email { get; }
        public string Password { get; }
        public string PasswordSalt { get; }

        public UserCreatedIntegrationEvent(string email, string password, string passwordSalt)
        {
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
        }
    }
}