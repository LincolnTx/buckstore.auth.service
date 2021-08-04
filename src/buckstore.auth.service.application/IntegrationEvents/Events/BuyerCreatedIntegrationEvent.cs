namespace buckstore.auth.service.application.IntegrationEvents.Events
{
    public class BuyerCreatedIntegrationEvent : IntegrationEvent
    {
        public string Name { get; set; }
        public string Cpf { get; set; }

        public BuyerCreatedIntegrationEvent(string name, string cpf)
        {
            Name = name;
            Cpf = cpf;
        }
    }
}
