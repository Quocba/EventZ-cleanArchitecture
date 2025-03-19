using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.SumaryEvent.Queries.TotalNumberOfAttendees
{
    public class TotalNumberOfAttendeesQuery : IRequest<PagedResponseWithTotal<List<TotalNumberOfAttendessDTO>>>
    {
        public int pageSize { get; set; } = 10;
        public int pageNUmber { get; set; } = 1;
        public Guid EventID { get; set; }

        public TotalNumberOfAttendeesQuery() { }
        public TotalNumberOfAttendeesQuery(Guid eventID)
        {
            EventID = eventID;
        }
    }
}
