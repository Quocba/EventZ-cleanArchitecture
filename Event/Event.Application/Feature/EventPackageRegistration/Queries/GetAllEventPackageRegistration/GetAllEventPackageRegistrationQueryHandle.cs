using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using Event.Application.Exceptions;
using MediatR;

namespace Event.Application.Feature.EventPackageRegistration.Queries.GetAllEventPackageRegistration
{
    public class GetAllEventPackageRegistrationQueryHandler(IEventPackageRegistrationQuery _eventPackageRegistrationQuery)
    : IRequestHandler<GetAllEventPackageRegistrationQuery, PagedResponse<List<GetEventPackageRegistrationDTO>>>
    {
        public async Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> Handle(GetAllEventPackageRegistrationQuery request, CancellationToken cancellationToken)
        {
            int? statusFilter = null;

            if (!string.IsNullOrEmpty(request.Status) &&
                Enum.TryParse<EventPackageRegistrationStatusEnum>(request.Status, true, out var parsedStatus))
            {
                statusFilter = (int)parsedStatus;
            }

            return await _eventPackageRegistrationQuery.GetAllEventPackageRegistration(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            }, statusFilter);
        }
    }
}
