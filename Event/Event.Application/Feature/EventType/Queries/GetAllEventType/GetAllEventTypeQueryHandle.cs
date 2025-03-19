using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Feature.EventType.Queries.GetAllEventType
{
    public class GetAllEventTypeQueryHandle(IEventTypeQuery _eventTypeQuery) : IRequestHandler<GetAllEventTypeQuery, PagedResponse<List<Domain.Entities.EventType>>>
    {
        public async Task<PagedResponse<List<Domain.Entities.EventType>>> Handle(GetAllEventTypeQuery request, CancellationToken cancellationToken)
        {
            return await _eventTypeQuery.GetAllEventType(new PagedRequest
            {
                PageNumber = 1,
                PageSize = 10,
            });
        }
    }
}
