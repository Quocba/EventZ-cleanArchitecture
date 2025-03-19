using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using Event.Infrastructure.Shares;
using MediatR;

namespace Event.Application.Feature.Events.Command.ConfirmEvent
{
    public class ConfirmEventCommandHandle(IRepository<Domain.Entities.Events> _eventRepository, IRepository<
       Domain.Entities.EventRegistrationLink> _eventRegistrationRepository) : IRequestHandler<ConfirmEventCommand>
    {
        public async Task Handle(ConfirmEventCommand request, CancellationToken cancellationToken)
        {
            var getEvent = await _eventRepository.GetByIdAsync(request.EventID);

            if (getEvent == null)
            {
                throw new NotImplementedException();
            }

            getEvent.NumberOfGuest = request.NumberOfGuest;
            getEvent.Status = request.Status;
            getEvent.Price = getEvent.Price;
            await _eventRepository.UpdateAsync(getEvent);

            Domain.Entities.EventRegistrationLink create = new Domain.Entities.EventRegistrationLink
            {
                Id = Guid.NewGuid(),
                Code = Util.GenerateUniqueString(6),
                StartDate = request.EventRegistrationLink.StartDate,
                EndDate = request.EventRegistrationLink.EndDate,
                EventID = getEvent.Id,

            };

            await _eventRegistrationRepository.AddAsync(create);
            await _eventRegistrationRepository.SaveAsync();

        }
    }
}
