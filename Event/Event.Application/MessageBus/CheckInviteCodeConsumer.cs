using Event.Application.Feature.EventRegistrationLink.Commands.CheckInviteCode;
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
    public class CheckInviteCodeConsumer(ISender _sender) : IConsumer<CheckInviteCodeEvent>
    {
        public async Task Consume(ConsumeContext<CheckInviteCodeEvent> context)
        {
            var eventId = await _sender.Send(new CheckInviteCodeCommand() { InviteCode = context.Message.InviteCode});

            await context.RespondAsync(new CheckInviteCodeResponse { EventId = eventId });
        }
    }
}
