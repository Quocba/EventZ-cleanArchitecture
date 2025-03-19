using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities;
using MediatR;
using Event.Application.Interfaces;
using Event.Application.Feature.Events.Command.CreateEvent;

namespace Event.Application.Feature.Events
{
    public class CreateEventCommandHandle(IEventRepository _eventRepository, IRepository<EventImages> _eventImageRepository) 
        : IRequestHandler<CreateEventCommand, Guid>
    {
        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {


            var id = Guid.NewGuid();

            Event.Domain.Entities.Events createEvent = new Event.Domain.Entities.Events
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Province = request.Province,
                Address = request.Address,
                AdditionalInfo = request.AdditionalInfo,
                NumberOfGuest = request.NumberOfGuest,
                IsActive = request.IsActive,
                IsOpenLayout = request.IsOpenLayout,
                Status = request.Status,
                Price = 0,
                EventTypeID = request.EventTypeID,
                UserID = request.UserID
            };
            await _eventRepository.AddAsync(createEvent);

            foreach (var image in request.Images)
            {

                EventImages addEventImage = new EventImages
                {
                    Id = Guid.NewGuid(),
                    EventID = createEvent.Id,   
                    ImageType = image.ImageType,
                    ImageUrl = image.ImageUrl,
                };
                await _eventImageRepository.AddAsync(addEventImage);
                await _eventRepository.SaveAsync();

            }

            return id;
        }
    }
}
