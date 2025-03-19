using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using MediatR;

namespace Event.Application.Feature.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandle(IEventQuery _eventQuery) : IRequestHandler<GetEventDetailQuery, GetEventDetailDTO>
    {

        public async Task<GetEventDetailDTO> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventQuery.GetEventDetail(request.EventID);
            return result;
        }
    }
}
