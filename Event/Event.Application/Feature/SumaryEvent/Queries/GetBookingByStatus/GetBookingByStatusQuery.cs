using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.SumaryEvent.Queries.GetBookingByStatus
{
    public class GetBookingByStatusQuery : IRequest<PagedResponseWithTotal<List<TotalNumberOfAttendessDTO>>>
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public Guid EventID { get; set; }
        public int Status {  get; set; }
        public GetBookingByStatusQuery() { }

        public GetBookingByStatusQuery(Guid eventID, int status)
        {
            EventID = eventID;
            Status = status;
        }
    }
}
