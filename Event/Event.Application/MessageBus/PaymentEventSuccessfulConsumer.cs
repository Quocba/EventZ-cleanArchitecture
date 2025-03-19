using Event.Application.Feature.Events.Command.PaymentEvent;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.MessageBus
{
    public class PaymentEventSuccessfulConsumer(ISender _sender) : IConsumer<PaymentEventSuccessfulEvent>
    {
        public async Task Consume(ConsumeContext<PaymentEventSuccessfulEvent> context)
        {
            await _sender.Send(new PaymentEventCommand() { EventID = context.Message.EventId });
        }
    }
}
