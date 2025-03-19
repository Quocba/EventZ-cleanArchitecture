using Event.Application.DTO;
using Event.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Interface
{
    public interface ILayoutQuery
    {
        Task<PagedResponse<List<LayoutResponse>>> GetAllLayout(PagedRequestWithSearch request);
        Task<LayoutResponse> GetLayout(Guid layoutId);
    }
}
