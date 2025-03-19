using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.Events.Queries.GetEventByUser
{
    public class GetEventByUserQueryHandle(IEventQuery _eventQuery) : IRequestHandler<GetEventByUserQuery, PagedResponse<List<GetAllEventDTO>>>
    {
        public async Task<PagedResponse<List<GetAllEventDTO>>> Handle(GetEventByUserQuery request, CancellationToken cancellationToken)
        {
            return await _eventQuery.GetEventByUser(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            }, request.UserID);
        }
    }
}
