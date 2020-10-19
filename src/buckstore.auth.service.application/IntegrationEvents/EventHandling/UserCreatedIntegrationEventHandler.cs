using System.Threading;
using System.Threading.Tasks;
using buckstore.auth.service.application.EventHandlers;
using buckstore.auth.service.application.IntegrationEvents.Events;

namespace buckstore.auth.service.application.IntegrationEvents.EventHandling
{
    // change to use IIntegrationEventHandler<UserCreatedIntegrationEvent>
    public class UserCreatedIntegrationEventHandler : EventHandler<UserCreatedIntegrationEvent>
    {
        public override Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            // insert info on Mongo
            throw new System.NotImplementedException();
        }
    }
}