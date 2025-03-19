using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.DTO;
using MassTransit;
using RabbitMQ.Contract.DTO;

namespace Identity.Application.MessageBus
{
    public class GetEventUpComingRequest(IRequestClient<GetUpComingEventRequest> _client)
    {

        public async Task<GetUpcomingEventResponse> GetUpcomingEventsAsync()
        {
            var response = await _client.GetResponse<GetUpcomingEventResponse>(new EventDTO());
            return response.Message;
        }
    }
}
