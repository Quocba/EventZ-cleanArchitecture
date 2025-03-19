using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;

namespace Event.Application.Interface
{
    public interface IEventTimeLineQuery
    {
        Task<PagedResponse<List<EventTimeLineDTO>>> GetTimeLineByEvent(PagedRequest request, Guid eventID, string? status);
        Task<PagedResponse<List<GetHandleByEventTimeLineDTO>>> GetHandleByEventTimeLine(PagedRequest request);
    }
}
