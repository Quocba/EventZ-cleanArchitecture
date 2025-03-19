using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using MassTransit;
using RabbitMQ.Contract.DTO;
using RabbitMQ.Contract.DTO.GetEvent;

namespace EventProduct.Application.MessageBus
{
    public class GetUpComingEventMessage(IRequestClient<GetEventRequest> _client)
    {
        public async Task<GetEventDetailResponse> GetUpcomingEventsAsync()
        {
            var response = await _client.GetResponse<GetEventDetailResponse>(new GetEventDTO());
            return response.Message;
        }
    }
}
