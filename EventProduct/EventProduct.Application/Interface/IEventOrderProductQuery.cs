using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;

namespace EventProduct.Application.Interface
{
    public interface IEventOrderProductQuery
    {
      Task<PagedResponse<List<GetEventOrderProductDTO>>> GetEventOrderProductByEventOrder(PagedRequest request, Guid eventOrderID);
        Task<int> StatisticalProductSaled(Guid eventID);
    }
}
