using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Interfaces;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Feature.EventPackage.Queries.GetRegistrationByEventPackage
{
    public class GetRegistrationByEventPackageQueryHandle(IEventPackageRegistrationQuery _eventPackageRegistrationQuery) : IRequestHandler<GetRegistrationByEventPackageQuery, PagedResponse<List<GetEventPackageRegistrationDTO>>>
    {
        public async Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> Handle(GetRegistrationByEventPackageQuery request, CancellationToken cancellationToken)
        {
       
            return await _eventPackageRegistrationQuery.GetRegistrationByEventPackage(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            }, request.EventPackageID);
        }
    }
}
