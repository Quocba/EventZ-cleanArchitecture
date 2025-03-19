using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventProductOrder.Queries.GetEventProductOrderByEventOrder
{
    public class GetEventProductOrderByEventOrderQuery : IRequest<PagedResponse<List<GetEventOrderProductDTO>>>
    {
        public int PageNumber = 1;
        public int PageSize = 10;
        public Guid EventOrderID { get; set; }
        public GetEventProductOrderByEventOrderQuery() { }

        public GetEventProductOrderByEventOrderQuery(Guid eventOrderID)
        {
            EventOrderID = eventOrderID;
        }
    }
}
