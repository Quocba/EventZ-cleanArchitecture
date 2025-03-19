using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;

namespace EventProduct.Application.Interface
{
    public interface ICategoryQuery
    {
        Task<PagedResponse<List<GetAllCategoryDTO>>> GetAllCategory(PagedRequest request);
        Task<PagedResponse<List<GetAllCategoryDTO>>> GetCategoryByEvent(PagedRequest request, Guid eventID);
    }
}
