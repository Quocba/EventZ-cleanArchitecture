using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interfaces;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Command.AddEventTimeLine
{
    public class AddEventTimeLineCommandHandle(IRepository<Domain.Entities.EventTimeLine> _eventTimeLineRepository) : IRequestHandler<AddEventTimeLineCommand, Guid>
    {
        public async Task<Guid> Handle(AddEventTimeLineCommand request, CancellationToken cancellationToken)
        {

            var addTimeLine = new Domain.Entities.EventTimeLine
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                StartDate = request.Start_Date,
                EndDate = request.End_Date,
                HandleBy = request.HandleBy,
                Status = request.Status,
                TimeLineType = request.TimeLineType,
                EventID = request.EventID,
                ParentID = request.ParentID,
            };

            await _eventTimeLineRepository.AddAsync(addTimeLine);
            await _eventTimeLineRepository.SaveAsync();
            return addTimeLine.Id;
        }
    }
}
