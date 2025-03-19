using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Feature.Events.Queries.GetAllEvents;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.SumaryEvent.Queries.EventWaitForApproval
{
    public class SumaryEventWithStatusQueryHandle(IEventQuery _eventQuery) : IRequestHandler<SumaryEventWithStatusQuery, GetEventByStatusDTO>
    {
        public async Task<GetEventByStatusDTO> Handle(SumaryEventWithStatusQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventQuery.SumaryEventWithStatus(request.status);

            return new GetEventByStatusDTO
            {
                Status = (EventStatusEnum)request.status,
                Quantity = result
            };
        }
    }
}
