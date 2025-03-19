using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Command.DeleteEventTimeLine
{
    public class DeleteEventTimeLineCommandHandle(IRepository<Domain.Entities.EventTimeLine> _eventTimeLineRepository) : IRequestHandler<DeleteEventTimeLineCommand>
    {
        public async Task Handle(DeleteEventTimeLineCommand request, CancellationToken cancellationToken)
        {
            var checkDelete = await _eventTimeLineRepository.GetByIdAsync(request.EventTimeLineID);
            if (checkDelete == null)
            {
                throw new KeyNotFoundException("Event time line not found");
            }
            await _eventTimeLineRepository.DeleteAsync(checkDelete);
            await _eventTimeLineRepository.SaveAsync();
        }
    }
}
