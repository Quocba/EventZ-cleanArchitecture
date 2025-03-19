using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Feature.EventPackageRegistration.Queries.GetAllEventPackageRegistration
{
    public class GetAllEventPackageRegistrationQuery : IRequest<PagedResponse<List<GetEventPackageRegistrationDTO>>>
    {
        public int PageNumber = 1;
        public int PageSize = 10;
        public string? Status { get; set; }

        public GetAllEventPackageRegistrationQuery(string? status)
        {
            Status = status;
        }
    }
}
