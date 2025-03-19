using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventOrder.Queries.GetEventOrderByEvent
{
    public class GetEventOrderByEventQuery : IRequest<PagedResponse<List<EventOrderDTO>>>
    {
        public int PageNumber = 1;
        public int PageSize = 10;
        public Guid EventID { get; set; }

        public GetEventOrderByEventQuery() { }

        public GetEventOrderByEventQuery(Guid eventID)
        {
            EventID = eventID;
        }
    }
}
