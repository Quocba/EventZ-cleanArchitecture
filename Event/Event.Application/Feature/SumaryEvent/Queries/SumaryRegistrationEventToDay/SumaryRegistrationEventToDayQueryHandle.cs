using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.SumaryEvent.Queries.SumaryRegistrationEventToDay
{
    public class SumaryRegistrationEventToDayQueryHandle(IEventPackageRegistrationQuery _eventPackageRegistrationQuery)
        : IRequestHandler<SumaryRegistrationEventToDayQuery, PagedResponse<List<GetEventPackageRegistrationDTO>>>
    {
        public async Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> Handle(SumaryRegistrationEventToDayQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventPackageRegistrationQuery.GetEventPackageRegistrationToDay(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            });

            return result;
        }
    }
}
