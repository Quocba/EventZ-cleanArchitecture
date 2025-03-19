using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using MediatR;

namespace Event.Application.Feature.Events.Queries.GetListEvent
{
    public class GetUpcomingEventQuery : IRequest<List<EventDTO>>
    {

    }
}
