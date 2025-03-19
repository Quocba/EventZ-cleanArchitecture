using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.EventPackageRegistration.Commands.EditEventPackageRegistrationStatus
{
    public class EditEventPackageRegistrationStatusHandle(IEventPackageRegistrationRepository _eventPackageRegistrationRepository) : IRequestHandler<EditEventPackageRegistrationStatusCommand>

    {

        async Task IRequestHandler<EditEventPackageRegistrationStatusCommand>.Handle(EditEventPackageRegistrationStatusCommand request, CancellationToken cancellationToken)
        {
            var checkUpdate = await _eventPackageRegistrationRepository.GetByID(request.EventPackageRegistrationID);
            if (checkUpdate == null)
            {
                throw new KeyNotFoundException($"Event Package Registration not found");
            }
            if (!Enum.TryParse<EventPackageRegistrationStatusEnum>(request.Status, true, out var parsedStatus))
            {
                throw new ArgumentException("Invalid status value", nameof(request.Status));
            }
            request.Status = parsedStatus.ToString(); 

            checkUpdate.Status = parsedStatus != 0 ? parsedStatus : checkUpdate.Status;
            checkUpdate.Price = request.Price != 0 ? request.Price : checkUpdate.Price;
            await _eventPackageRegistrationRepository.UpdateAsync(checkUpdate);
            await _eventPackageRegistrationRepository.SaveAsync();
        }
    }
}
