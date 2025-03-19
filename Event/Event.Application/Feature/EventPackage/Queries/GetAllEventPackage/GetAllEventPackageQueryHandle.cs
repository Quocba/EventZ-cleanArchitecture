using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Feature.EventPackage.Queries.GetAllEventPackage
{
    public class GetAllEventPackageQueryHandle(IEventPackageQuery _eventPackageQuery) : IRequestHandler<GetAllEventPackageQuery, PagedResponse<List<EventPackages>>>
    {

        public async Task<PagedResponse<List<EventPackages>>> Handle(GetAllEventPackageQuery request, CancellationToken cancellationToken)
        {
            return await _eventPackageQuery.GetAllEventPackage(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            });
        }
    }
}
