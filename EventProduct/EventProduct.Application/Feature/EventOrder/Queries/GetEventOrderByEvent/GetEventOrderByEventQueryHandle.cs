using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Feature.EventOrder.Queries.GetEventOrderByEvent;
using EventProduct.Application.Wrappers;
using EventProduct.Domain.Entities;
using MediatR;

namespace EventProduct.Application.Feature.EventOrder.Queries.GetEventOrderByPayment
{
    public class GetEventOrderByEventQueryHandle(IEventOrderQuery _eventOrderQuery) : IRequestHandler<GetEventOrderByEventQuery, PagedResponse<List<EventOrderDTO>>>
    {
        public async Task<PagedResponse<List<EventOrderDTO>>> Handle(GetEventOrderByEventQuery request, CancellationToken cancellationToken)
        {
            return await _eventOrderQuery.GetEventOrderByEvent(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize

            },request.EventID);
        }
    }
}
