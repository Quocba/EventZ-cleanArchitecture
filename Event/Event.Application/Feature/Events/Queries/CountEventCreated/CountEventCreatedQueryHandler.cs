using Event.Application.DTO;
using Event.Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Events.Queries.CountEventCreated
{
    public class CountEventCreatedQueryHandler(IEventQuery _eventQuery) : IRequestHandler<CountEventCreatedQuery, int>
    {
        public async Task<int> Handle(CountEventCreatedQuery request, CancellationToken cancellationToken)
        {
            return await _eventQuery.CountEventCreated(request.UserId);
        }
    }
}
