using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.EventType.Queries.GetAllEventType
{
    public class GetAllEventTypeQuery : IRequest<PagedResponse<List<Domain.Entities.EventType>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
