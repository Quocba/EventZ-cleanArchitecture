using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Interface;
using EventProduct.Domain.Entities;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DTO;
using RabbitMQ.Contract.DTO.GetEvent;

namespace EventProduct.Application.Feature.Statistical.Total_Revenue
{
    public class TotalRevenueProductQueryHandle(IEventOrderQuery _eventOrderQuery, IRequestClient<GetEventRequest> _requestClient)
        : IRequestHandler<TotalRevenueProductQuery, TotalRevenueProductDTO>
    {
        public async Task<TotalRevenueProductDTO> Handle(TotalRevenueProductQuery request, CancellationToken cancellationToken)
        {
            var totalRevenue = await _eventOrderQuery.TotalRevenueProduct(request.EventID);
            var getEventInfo = await _requestClient.GetResponse<GetEventDetailResponse>(new GetEventRequest { eventID = request.EventID });
      
            return new TotalRevenueProductDTO
            {
                EventID = getEventInfo.Message.EventID,
                Title = getEventInfo.Message.Title,
                TotalRevenue = totalRevenue,
            };
        }
    }
}
