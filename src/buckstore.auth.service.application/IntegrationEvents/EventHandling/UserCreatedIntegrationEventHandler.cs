using System.Threading.Tasks;
using buckstore.auth.service.application.IntegrationEvents.Events;

namespace buckstore.auth.service.application.IntegrationEvents.EventHandling
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public Task Handle(UserCreatedIntegrationEvent @event)
        {
            // insert new user no mongo db
            throw new System.NotImplementedException();
        }
    }
}