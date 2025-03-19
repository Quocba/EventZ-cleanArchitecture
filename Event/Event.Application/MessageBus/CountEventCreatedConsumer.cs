using Event.Application.Feature.EventRegistrationLink.Commands.CheckInviteCode;
using Event.Application.Feature.Events.Queries.CountEventCreated;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.MessageBus
{
    public class CountEventCreatedConsumer(ISender _sender) : IConsumer<CountEventCreatedEvent>
    {
        public async Task Consume(ConsumeContext<CountEventCreatedEvent> context)
        {
            var numberOfCreated = await _sender.Send(new CountEventCreatedQuery() { UserId = context.Message.UserId });

            await context.RespondAsync(new CountEventCreatedResponse { NumberOfEventsCreated = numberOfCreated });
        }
    }
}
