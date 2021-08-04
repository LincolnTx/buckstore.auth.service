using System;

namespace buckstore.auth.service.application.IntegrationEvents.Events
{
    public class BuyerCreatedIntegrationEvent : IntegrationEvent
    {
        public new Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }

        public BuyerCreatedIntegrationEvent(string name, string cpf, Guid id)
        {
            Name = name;
            Cpf = cpf;
            Id = id;
        }
    }
}
