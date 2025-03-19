using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Feature.EventImage.Command.DeleteEventImage
{
    public class DeleteEventImageCommandHandle(IRepository<EventImages> _eventImageRepository) : IRequestHandler<DeleteEventImageCommand>
    {
        public async Task Handle(DeleteEventImageCommand request, CancellationToken cancellationToken)
        {
            var getImage = await _eventImageRepository.GetByIdAsync(request.EventImageID);
            await _eventImageRepository.DeleteAsync(getImage);
            await _eventImageRepository.SaveAsync();
        }
    }
}
