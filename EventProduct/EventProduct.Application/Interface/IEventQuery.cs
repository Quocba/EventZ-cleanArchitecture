using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;

namespace EventProduct.Application.Interface
{
    public interface IEventQuery
    {
        Task<PagedResponse<List<GetAllEventDTO>>> GetAllEvent(PagedRequest request, string? title, bool? isDeleted);
        Task<GetEventDetailDTO> GetEventDetail(Guid eventID);

        Task<PagedResponse<List<GetAllEventDTO>>> GetEventByUser(PagedRequest request, Guid userId, string? title, bool? isDeleted);
    }
}
