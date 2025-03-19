using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Command.EditEventTimeLine
{
    public class EditEventTimeLineCommandHandle(IRepository<Domain.Entities.EventTimeLine> _eventTimeLineRepository) : IRequestHandler<EditEventTimeLineCommand>
    {
        public async Task Handle(EditEventTimeLineCommand request, CancellationToken cancellationToken)
        {
            var checkUpdate = await _eventTimeLineRepository.GetByIdAsync(request.EventTimeLineID);
            if (checkUpdate == null)
            {
                throw new KeyNotFoundException("Event time line not found");
            }

            checkUpdate.Title = request.Title ?? checkUpdate.Title;
            checkUpdate.Content = request.Content ?? checkUpdate.Content;
            checkUpdate.StartDate = request.StartDate == null ? request.StartDate : checkUpdate.StartDate;
            checkUpdate.EndDate = request.EndDate == null ? request.EndDate : checkUpdate.EndDate;
            checkUpdate.HandleBy = request.HandleBy ?? checkUpdate.HandleBy;
            checkUpdate.TimeLineType = request.TimeLineType;
            await _eventTimeLineRepository.UpdateAsync(checkUpdate);
            await _eventTimeLineRepository.SaveAsync();
        }
    }
}
