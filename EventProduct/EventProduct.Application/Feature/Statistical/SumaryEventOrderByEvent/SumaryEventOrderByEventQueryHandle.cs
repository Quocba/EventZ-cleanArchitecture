using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Domain.Entities;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DTO;
using RabbitMQ.Contract.DTO.GetEvent;

namespace EventProduct.Application.Feature.Statistical.EventOrder
{
    public class SumaryEventOrderByEventQueryHandle(IEventOrderQuery _query, IRequestClient<GetEventRequest> _requestClient) : IRequestHandler<SumaryEventOrderByEventQuery, TotalEventOrderDTO>
    {
        public async Task<TotalEventOrderDTO> Handle(SumaryEventOrderByEventQuery request, CancellationToken cancellationToken)
        {
            var result = await _query.SumaryEventOrder(request.eventID);
            var eventDetail = await _requestClient.GetResponse<GetEventDetailResponse>(new GetEventRequest {eventID = request.eventID });
            return new TotalEventOrderDTO
            {
                EventId = eventDetail.Message.EventID,
                EventName = eventDetail.Message.Title,
                Total = result
            };
        }
    }
}
