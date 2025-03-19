using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Feature.Events.Queries.GetEventDetail;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DTO;
using RabbitMQ.Contract.DTO.GetEvent;

namespace Event.Application.MessageBus.GetEventByID
{
    public class GetEventDetailConsumer(ISender _sender) : IConsumer<GetEventRequest>
    {
        public async Task Consume(ConsumeContext<GetEventRequest> context)
        {
            var request = context.Message;
            var eventDetail = await _sender.Send(new GetEventDetailQuery { EventID = request.eventID});
            if (eventDetail == null)
            {
                await context.RespondAsync(new { Message = "Event not found", Success = false });
                return;
            }
            var response = new GetEventDetailResponse
            {
                EventID = eventDetail.Id,
                Title = eventDetail.Title
            };
            Console.WriteLine("Process Get Event Success");
            await context.RespondAsync(response);

        }
    }
}
