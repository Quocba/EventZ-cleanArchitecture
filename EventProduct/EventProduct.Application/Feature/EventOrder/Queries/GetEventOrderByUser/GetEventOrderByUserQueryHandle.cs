using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;
using EventProduct.Domain.Entities;
using MediatR;

namespace EventProduct.Application.Feature.EventOrder.Queries.GetEventOrderByPayment
{
    public class GetEventOrderByUserQueryHandle(IEventOrderQuery _eventOrderQuery) : IRequestHandler<GetEventOrderByUserQuery, PagedResponse<List<EventOrderDTO>>>
    {
        public async Task<PagedResponse<List<EventOrderDTO>>> Handle(GetEventOrderByUserQuery request, CancellationToken cancellationToken)
        {
            return await _eventOrderQuery.GetEventOrderByUser(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize

            },request.UserID);
        }
    }
}
