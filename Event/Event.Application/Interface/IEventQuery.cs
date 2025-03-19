using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using Event.Domain.Entities;

namespace Event.Application.Interface
{
    public interface IEventQuery
    {
        Task<PagedResponse<List<GetAllEventDTO>>> GetAllEvent(PagedRequestWithSearch request,string? status);
        Task<PagedResponse<List<GetAllEventDTO>>> GetEventByUser(PagedRequest request,Guid userID);
        Task<GetEventDetailDTO> GetEventDetail(Guid eventID);
        Task<int> CountEventCreated(Guid? userId);
        Task<List<EventDTO>> GetUpComingEvent();

        Task<int> SumaryEventWithStatus(int status);
    }
}
