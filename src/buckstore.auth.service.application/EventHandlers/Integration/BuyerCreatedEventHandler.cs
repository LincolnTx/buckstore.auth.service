using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.auth.service.application.Adapters.MessageBroker;
using buckstore.auth.service.application.IntegrationEvents.Events;

namespace buckstore.auth.service.application.EventHandlers.Integration
{
    public class BuyerCreatedEventHandler : EventHandler<BuyerCreatedIntegrationEvent>
    {
        private readonly IMessageProducer<BuyerCreatedIntegrationEvent> _producer;
        private readonly ILogger<BuyerCreatedEventHandler> _logger;

        public BuyerCreatedEventHandler(IMessageProducer<BuyerCreatedIntegrationEvent> producer, ILogger<BuyerCreatedEventHandler> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public override async Task Handle(BuyerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _producer.Produce(notification);
            _logger.LogInformation("Comprador gerado e enviado pra a api de orders");
        }
    }
}
