using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.EventPackage.Queries.GetRegistrationByEventPackage
{
    public class GetRegistrationByEventPackageQuery : IRequest<PagedResponse<List<GetEventPackageRegistrationDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid EventPackageID {  get; set; }

        public GetRegistrationByEventPackageQuery() { }

        public GetRegistrationByEventPackageQuery(Guid eventPackageID) { 
            EventPackageID = eventPackageID;
        }
    }
}
