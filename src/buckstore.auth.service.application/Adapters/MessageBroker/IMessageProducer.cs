using System.Threading.Tasks;
using buckstore.auth.service.application.IntegrationEvents.Events;

namespace buckstore.auth.service.application.Adapters.MessageBroker
{
    public interface IMessageProducer<in TEvent> where TEvent : IntegrationEvent
    {
        Task Produce(TEvent message);
    }
}
