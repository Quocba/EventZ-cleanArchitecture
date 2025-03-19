using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Feature.Events.Queries.GetListEvent;
using Event.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Contract.DTO;

namespace Event.Application.MessageBus.GetUpComingEvent
{
    public class GetUpComingEventConsumer(ISender _sender) : IConsumer<GetUpComingEventRequest>
    {
        public async Task Consume(ConsumeContext<GetUpComingEventRequest> context)
        {

            var listEvent = await _sender.Send(new GetUpcomingEventQuery());
            var upComingEvents = listEvent.Select(e => new UpComingEventDTO
            {
                Id = e.Id,
                Title = e.Title,
                StartTime = e.StartTime,
                Address = e.Address
            }).ToList();
            await context.RespondAsync(new GetUpcomingEventResponse
            {
                Events = upComingEvents
            });
        }
    }
}
