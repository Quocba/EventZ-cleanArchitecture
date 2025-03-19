using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventType.Queries.GetByID
{
    public class GetByIDQuery : IRequest<Domain.Entities.EventType>
    {
        public Guid EventTypeID { get; set; }
        public GetByIDQuery() { }
        public GetByIDQuery(Guid eventTypeID)
        {
            EventTypeID = eventTypeID;
        }
    }
}
