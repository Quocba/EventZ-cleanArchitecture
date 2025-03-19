using Event.Application.Feature.Events.Queries.GetEventDetail;
using Event.Domain.Entities.Enum;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.MessageBus
{
    public class GetEventConsumer(ISender _sender) : IConsumer<GetEventEvent>
    {
        public async Task Consume(ConsumeContext<GetEventEvent> context)
        {
            var eventDetail = await _sender.Send(new GetEventDetailQuery() { EventID = context.Message.EventId });

            await context.RespondAsync(new GetEventResponse
            {
                Id = eventDetail.Id,
                Title = eventDetail.Title,
                Price = eventDetail.Price,
                IsApproved = eventDetail.Status == EventStatusEnum.WAIT_FOR_PAYMENT
            });
        }
    }
}
