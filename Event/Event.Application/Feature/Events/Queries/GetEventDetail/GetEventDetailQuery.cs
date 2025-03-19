using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using MediatR;

namespace Event.Application.Feature.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<GetEventDetailDTO>
    {
        public Guid EventID { get; set; }

        public GetEventDetailQuery() { }
        public GetEventDetailQuery(Guid eventID)
        {
            EventID = eventID;
        }
    }
}
