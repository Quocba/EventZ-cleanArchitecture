using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using MediatR;

namespace Event.Application.Feature.EventRegistrationLink.Queries.GetRegistrationLinkByEvent
{
    public class GetRegistrationLinkByEventQueryHandle(IEventRegistrationLinkQuery _eventRegistrationLinkQuery)
        : IRequestHandler<GetRegistrationLinkByEventQuery, List<GetEventRegistrationLinkDTO>>
    {
        public async Task<List<GetEventRegistrationLinkDTO>> Handle(GetRegistrationLinkByEventQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventRegistrationLinkQuery.GetEventRegistrationLinks(request.EventID);
            return result;
        }
    }
}
