using EmailService.Config;
using EmailService.MessageBus;
using MassTransit;
using System.Reflection;

namespace EmailService.DependencyConfig
{
    public static class AddConfigRabbitMQ
    {
        public static IServiceCollection AddConfigMasstransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var masstransitConfiguration = new RabbitMQConfig();
            configuration.GetSection(RabbitMQConfig.ConfigName).Bind(masstransitConfiguration);

            services.AddMassTransit(mt =>
            {
                mt.AddConsumer<SendEmailConsumer>();

                mt.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                    {
                        h.Username(masstransitConfiguration.Username);
                        h.Password(masstransitConfiguration.Password);
                    });

                    bus.ConfigureEndpoints(context);

                    bus.ReceiveEndpoint("send-email-queue", e =>
                    {
                        e.ConfigureConsumer<SendEmailConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
