using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Interface;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventProductOrder.Queries.GetEventProductOrderByEventOrder
{
    public class GetEventProductOrderByEventOrderQueryHandle(IEventOrderProductQuery _eventOrderProductQuery)
        : IRequestHandler<GetEventProductOrderByEventOrderQuery, PagedResponse<List<GetEventOrderProductDTO>>>
    {
        public async Task<PagedResponse<List<GetEventOrderProductDTO>>> Handle(GetEventProductOrderByEventOrderQuery request, CancellationToken cancellationToken)
        {
            return await _eventOrderProductQuery.GetEventOrderProductByEventOrder(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            }, request.EventOrderID);
        }
    }
}
