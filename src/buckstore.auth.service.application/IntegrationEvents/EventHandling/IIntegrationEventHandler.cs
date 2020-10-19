using System.Threading.Tasks;
using buckstore.auth.service.application.IntegrationEvents.Events;

namespace buckstore.auth.service.application.IntegrationEvents.EventHandling
{
    public interface IIntegrationEventHandler
    {
    }

    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}