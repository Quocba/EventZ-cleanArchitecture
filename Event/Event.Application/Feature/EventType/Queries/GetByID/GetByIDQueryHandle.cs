using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interface;
using MediatR;

namespace Event.Application.Feature.EventType.Queries.GetByID
{
    public class GetByIDQueryHandle(IEventTypeQuery _eventTypeQuery) : IRequestHandler<GetByIDQuery, Domain.Entities.EventType>
    {
        public Task<Domain.Entities.EventType> Handle(GetByIDQuery request, CancellationToken cancellationToken)
        {
            var getType = _eventTypeQuery.GetByID(request.EventTypeID);

            if (getType == null)
            {

                throw new NotImplementedException();
            }

            return getType;
        }
    }
}
