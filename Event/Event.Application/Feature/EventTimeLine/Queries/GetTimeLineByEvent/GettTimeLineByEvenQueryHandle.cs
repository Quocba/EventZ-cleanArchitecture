using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Queries.GetTimeLineByEvent
{
    public class GettTimeLineByEvenQueryHandle(IEventTimeLineQuery _eventTimeLineQuery) : IRequestHandler<GetTimeLineByEventQuery,PagedResponse<List<EventTimeLineDTO>>>
    {
        async Task<PagedResponse<List<EventTimeLineDTO>>> IRequestHandler<GetTimeLineByEventQuery, PagedResponse<List<EventTimeLineDTO>>>.Handle(GetTimeLineByEventQuery request, CancellationToken cancellationToken)
        {
            return await _eventTimeLineQuery.GetTimeLineByEvent(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            },
            request.EventID,
            request.Status);
        }
    }
}
