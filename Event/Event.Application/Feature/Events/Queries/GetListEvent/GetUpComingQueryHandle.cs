
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Feature.Events.Queries.GetAllEvents;
using Event.Application.Interface;
using MediatR;

namespace Event.Application.Feature.Events.Queries.GetListEvent
{
    public class GetUpComingQueryHandle(IEventQuery _eventQuery) : IRequestHandler<GetUpcomingEventQuery, List<EventDTO>>
    {
        public async Task<List<EventDTO>> Handle(GetUpcomingEventQuery request, CancellationToken cancellationToken)
        {
            return await _eventQuery.GetUpComingEvent();
        }
    }
}
