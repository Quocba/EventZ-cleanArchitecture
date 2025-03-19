using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Interface;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DTO;
using RabbitMQ.Contract.DTO.GetEvent;

namespace EventProduct.Application.Feature.Statistical.StatisticalProductSaled
{
    public class StatisticalProductSaledQueryHandle(IEventOrderProductQuery _orderProductQuery, IRequestClient<GetEventRequest> _reqeustClient)
        : IRequestHandler<StatisticalProductSaledQuery, StatisticalEventProductSaledDTO>
    {
        async Task<StatisticalEventProductSaledDTO> IRequestHandler<StatisticalProductSaledQuery, StatisticalEventProductSaledDTO>.Handle(StatisticalProductSaledQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderProductQuery.StatisticalProductSaled(request.EventID);
            var getEventInfo = await _reqeustClient.GetResponse<GetEventDetailResponse>(new GetEventRequest { eventID = request.EventID });

            return new StatisticalEventProductSaledDTO
            {
                EventID = getEventInfo.Message.EventID,
                EventTitle = getEventInfo.Message.Title,
                TotalProductSaled = result
            };
        }
    }
}
