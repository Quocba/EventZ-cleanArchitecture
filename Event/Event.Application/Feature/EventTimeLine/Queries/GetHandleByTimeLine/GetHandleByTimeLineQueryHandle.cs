using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Feature.EventTimeLine.Queries.GetTimeLineByEvent;
using Event.Application.Interface;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Queries.GetHandleByTimeLine
{
    public class GetHandleByTimeLineQueryHandle(IEventTimeLineQuery _eventTimeLineQuery) : IRequestHandler<GetHandleByTimeLineQuery, PagedResponse<List<GetHandleByEventTimeLineDTO>>>
    {
        public async Task<PagedResponse<List<GetHandleByEventTimeLineDTO>>> Handle(GetHandleByTimeLineQuery request, CancellationToken cancellationToken)
        {
            
            return await _eventTimeLineQuery.GetHandleByEventTimeLine(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            });

        }
    }
}
