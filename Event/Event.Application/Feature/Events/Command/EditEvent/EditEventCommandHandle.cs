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
using static System.Net.Mime.MediaTypeNames;

namespace Event.Application.Feature.Events.Command.EditEvent
{
    public class EditEventCommandHandle(IRepository<Domain.Entities.Events> _eventRepository,
                                        IRepository<EventImages> _eventImageRepository) : IRequestHandler<EditEventCommand>
    {
        public async Task Handle(EditEventCommand request, CancellationToken cancellationToken)
        {
            var checkUpdate = await _eventRepository.GetByIdAsync(request.EventID);
            if(checkUpdate == null)
            {
                throw new NotImplementedException("Event not found");
            }


            checkUpdate.Title = request.Title ?? checkUpdate.Title;
            checkUpdate.Description = request.Description ?? checkUpdate.Description;
            checkUpdate.StartTime = request.StartTime != null ? request.StartTime : checkUpdate.StartTime;
            checkUpdate.EndTime = request.EndTime != null ? request.EndTime : checkUpdate.EndTime; ;
            checkUpdate.Province = request.Province ?? checkUpdate.Province;
            checkUpdate.Address = request.Address ?? checkUpdate.Address;
            checkUpdate.AdditionalInfo = request.AdditionalInfo ?? checkUpdate.AdditionalInfo;
            checkUpdate.IsActive = request.IsActive != default ? request.IsActive : checkUpdate.IsActive;
            checkUpdate.IsOpenLayout = request.IsOpenLayout != default ? request.IsOpenLayout : checkUpdate.IsOpenLayout;
            checkUpdate.Status = checkUpdate.Status;
            checkUpdate.EventTypeID = request.EventTypeID != null ? request.EventTypeID : checkUpdate.EventTypeID;
            checkUpdate.UserID = checkUpdate.UserID;
            await _eventRepository.UpdateAsync(checkUpdate);
            foreach(var image in request.Images)
            {

                var addEventImage = new EventImages
                {
                    Id = Guid.NewGuid(),
                    EventID = checkUpdate.Id,
                    ImageType = image.ImageType,
                    ImageUrl = image.ImageUrl,
                };
                await _eventImageRepository.AddAsync(addEventImage);
            }
            await  _eventRepository.SaveAsync();
            
        }
    }
}
