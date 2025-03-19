using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;

namespace EventProduct.Application.Interface
{
    public interface IProductQuery 
    {
        Task<PagedResponse<List<GetAllProductDTO>>> GetAllProduct(PagedRequestWithSearch request, string? name);
        Task<PagedResponse<List<GetAllProductDTO>>> GetProductByCategory(PagedRequestWithSearch request, Guid categoryId,string? name);
    }
}
