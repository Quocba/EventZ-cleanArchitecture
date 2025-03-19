using Event.Application.Exceptions;
using Event.Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Events.Command.PaymentEvent
{
    public class PaymentEventCommandHandler(IEventRepository _eventRepository) : IRequestHandler<PaymentEventCommand>
    {
        public async Task Handle(PaymentEventCommand request, CancellationToken cancellationToken)
        {
            var eventDetail = await _eventRepository.GetByIdAsync(request.EventID);

            if (eventDetail == null)
            {
                throw new NotFoundException("Event not found");
            }

            eventDetail.Status = Domain.Entities.Enum.EventStatusEnum.PREPARING;

            await _eventRepository.UpdateAsync(eventDetail);
            await _eventRepository.SaveAsync();
        }
    }
}
