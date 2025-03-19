using Identity.Application.MessageBus;
using Identity.Infrastructure.RabbitMQ.Config;
using MassTransit;
using RabbitMQ.Contract.DTO;
using System.Reflection;

namespace Identity.API.DependencyConfig
{
    public static class AddConfigRabbitMQ
    {
        public static IServiceCollection AddConfigMasstransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var masstransitConfiguration = new RabbitMQConfig();
            configuration.GetSection(RabbitMQConfig.ConfigName).Bind(masstransitConfiguration);

            services.AddMassTransit(mt =>
            {
                mt.AddConsumer<SendMailConsumer>();
                mt.AddRequestClient<GetUpComingEventRequest>();
                mt.AddConsumer<GetUserConsumer>();
                mt.UsingRabbitMq((context, bus) =>
                {
             
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                    {
                        h.Username(masstransitConfiguration.Username);
                        h.Password(masstransitConfiguration.Password);
                    });
                    bus.ConfigureEndpoints(context);
                }); 
            });

            return services;
        }
    }
}
