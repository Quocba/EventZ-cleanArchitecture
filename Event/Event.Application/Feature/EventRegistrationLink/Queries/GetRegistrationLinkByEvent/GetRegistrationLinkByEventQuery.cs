using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using MediatR;

namespace Event.Application.Feature.EventRegistrationLink.Queries.GetRegistrationLinkByEvent
{
    public class GetRegistrationLinkByEventQuery : IRequest<List<GetEventRegistrationLinkDTO>>
    {
        public Guid EventID { get; set; }
        public GetRegistrationLinkByEventQuery() { }
        public GetRegistrationLinkByEventQuery(Guid eventID) { 

                EventID = eventID;
        }
    }
}
