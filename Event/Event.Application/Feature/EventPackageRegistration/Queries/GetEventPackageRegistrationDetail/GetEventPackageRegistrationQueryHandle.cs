using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using MediatR;

namespace Event.Application.Feature.EventPackageRegistration.Queries.GetEventPackageRegistrationDetail
{
    public class GetEventPackageRegistrationQueryHandle(IEventPackageRegistrationQuery _eventPackageRegistrationQuery) : IRequestHandler<GetEventRegistrationDetailQuery, GetEventPackageRegistrationDTO>
    {
        public Task<GetEventPackageRegistrationDTO> Handle(GetEventRegistrationDetailQuery request, CancellationToken cancellationToken)
        {
            var result = _eventPackageRegistrationQuery.GetEventPackageRegistrationDetail(request.eventPackageRegistrationID);
            if (result == null)
            {
                throw new KeyNotFoundException("Registration not found");
            }
            return result;
        }
    }
}
