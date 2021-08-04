using System;
using MassTransit;
using MassTransit.Registration;
using MassTransit.KafkaIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using buckstore.auth.service.environment.Configuration;
using buckstore.auth.service.application.IntegrationEvents.Events;

namespace buckstore.auth.service.infrastructure.CrossCutting.IoC.Configurations
{
    public static class KafkaSetup
    {
        private static KafkaConfiguration _kafkaConfiguration;

        public static void AddKafkaSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            _kafkaConfiguration = configuration.GetSection("KafkaConfiguration").Get<KafkaConfiguration>();

            services.AddMassTransit(bus =>
            {
                bus.UsingInMemory((ctx, cfg) => cfg.ConfigureEndpoints(ctx));

                bus.AddRider(rider =>
                {
                    rider.AddProducers();

                    rider.UsingKafka((ctx, kafka) =>
                    {
                        kafka.Host(_kafkaConfiguration.ConnectionString);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }

        static void AddProducers(this IRiderRegistrationConfigurator rider)
        {
            rider.AddProducer<BuyerCreatedIntegrationEvent>(_kafkaConfiguration.AuthToOrder);
        }
    }
}
