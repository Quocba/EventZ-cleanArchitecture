using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Feature.EventPackageRegistration.RegistrationEventPackge
{
    public class RegistrationEventPackageCommandHandle(IEventPackageRegistrationRepository _eventPackageRegistrationRepository) : IRequestHandler<RegistrationEventPackageCommand>
    {
        public async Task Handle(RegistrationEventPackageCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request can be not empty");
            }
            EventPackageRegistrations registrations = new EventPackageRegistrations
            {

                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Status = EventPackageRegistrationStatusEnum.Pending,
                CreateAt = request.CreateAt,
                UpdateAt = request.UpdateAt,
                Price = request.Price,
                EventPackageID = request.EventPackageID
            };
           await _eventPackageRegistrationRepository.AddAsync(registrations);
           await _eventPackageRegistrationRepository.SaveAsync();
            
        }
    }
}
