using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.Events.Queries.GetAllEvents
{
    public class GetAllEventQueryHandle(IEventQuery _eventQuery) : IRequestHandler<GetAllEventQuery, PagedResponse<List<GetAllEventDTO>>>
    {
        public async Task<PagedResponse<List<GetAllEventDTO>>> Handle(GetAllEventQuery request, CancellationToken cancellationToken)
        {
            return await _eventQuery.GetAllEvent(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            }, request.Status);

        }
    }
}
