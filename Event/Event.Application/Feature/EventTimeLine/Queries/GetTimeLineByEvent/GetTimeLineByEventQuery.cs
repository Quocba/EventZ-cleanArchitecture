using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Queries.GetTimeLineByEvent
{
    public class GetTimeLineByEventQuery : IRequest<PagedResponse<List<EventTimeLineDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid EventID { get; set; }
        public string? Status {  get; set; }

        public GetTimeLineByEventQuery() { }

        public GetTimeLineByEventQuery(Guid eventID, string? status)
        {
            EventID = eventID;
            Status = status;
        }
    }
}
