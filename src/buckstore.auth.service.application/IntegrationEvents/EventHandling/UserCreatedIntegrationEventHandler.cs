using System.Threading;
using System.Threading.Tasks;
using buckstore.auth.service.application.EventHandlers;
using buckstore.auth.service.application.IntegrationEvents.Events;
using buckstore.auth.service.application.Services;
using buckstore.auth.service.application.Services.AuthQueryServices;

namespace buckstore.auth.service.application.IntegrationEvents.EventHandling
{
    // change to use IIntegrationEventHandler<UserCreatedIntegrationEvent>
    public class UserCreatedIntegrationEventHandler : EventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IAuthQueryService _authQuery;

        public UserCreatedIntegrationEventHandler(IAuthQueryService authQuery)
        {
            _authQuery = authQuery;
        }

        public override async Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            // insert info on Mongo
            var user = new UserQueryModel(notification.Name, notification.Email, 
                notification.Password, notification.PasswordSalt, notification.Cpf);
            
            await _authQuery.Create(user);
        }
    }
}