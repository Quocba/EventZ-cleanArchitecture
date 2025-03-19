using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Feature.EventPackageRegistration.Queries.GetEventPackageRegistrationDetail
{
    public class GetEventRegistrationDetailQuery : IRequest<GetEventPackageRegistrationDTO>
    {
        [Required]
        public Guid eventPackageRegistrationID { get; set; }
        public GetEventRegistrationDetailQuery(Guid _eventPackageRegistrationID)
        {
            eventPackageRegistrationID = _eventPackageRegistrationID;
        }
    }
}
