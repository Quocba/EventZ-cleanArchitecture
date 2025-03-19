using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;

namespace Event.Application.Interface
{
    public interface IEventDocumentQuery
    {
        Task<PagedResponse<List<GetDocumentByEventDTO>>> GetDocumentsByEvent(PagedRequest request,Guid eventID);
    }
}
