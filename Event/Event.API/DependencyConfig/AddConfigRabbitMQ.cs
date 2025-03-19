using Event.Application.MessageBus;
using Event.Application.MessageBus.GetEventByID;
using Event.Application.MessageBus.GetUpComingEvent;
using Event.Infrastructure.RabbitMQ.Config;
using MassTransit;
using RabbitMQ.Contract.DTO;
using System.Reflection;

namespace Event.API.DependencyConfig
{
    public static class AddConfigRabbitMQ
    {
        public static IServiceCollection AddConfigMasstransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var masstransitConfiguration = new RabbitMQConfig();
            configuration.GetSection(RabbitMQConfig.ConfigName).Bind(masstransitConfiguration);

            services.AddMassTransit(mt =>
            {
                mt.AddConsumer<CheckInviteCodeConsumer>();
                mt.AddConsumer<GetEventConsumer>();
                mt.AddConsumer<PaymentEventSuccessfulConsumer>();
                mt.AddConsumer<CountEventCreatedConsumer>();
                mt.AddConsumer<GetUpComingEventConsumer>();
                mt.AddConsumer<SendMailInviteUserConsumer>();
                mt.AddConsumer<GetEventDetailConsumer>();
                mt.AddRequestClient<GetUserRequestDTO>();

                mt.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                    {
                        h.Username(masstransitConfiguration.Username);
                        h.Password(masstransitConfiguration.Password);
                    });

                    bus.ConfigureEndpoints(context);

                    bus.ReceiveEndpoint("check-invite-code-queue", e =>
                    {
                        e.ConfigureConsumer<CheckInviteCodeConsumer>(context);
                    });
                    bus.ReceiveEndpoint("get-event-queue", e =>
                    {
                        e.ConfigureConsumer<GetEventConsumer>(context);
                    });
                    bus.ReceiveEndpoint("payment-event-successful-queue", e =>
                    {
                        e.ConfigureConsumer<PaymentEventSuccessfulConsumer>(context);
                    });
                    bus.ReceiveEndpoint("count-event-created-queue", e =>
                    {
                        e.ConfigureConsumer<CountEventCreatedConsumer>(context);
                    });

                    bus.ReceiveEndpoint("get-event-upcoming-queue", e =>
                    {
                        e.ConfigureConsumer<GetUpComingEventConsumer>(context);
                    });

                });
            });

            return services;
        }
    }
}
