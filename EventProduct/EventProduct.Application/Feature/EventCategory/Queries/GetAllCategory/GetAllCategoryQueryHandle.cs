using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Interface;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandle(ICategoryQuery _categoryQuery) : IRequestHandler<GetAllCategoryQuery, PagedResponse<List<GetAllCategoryDTO>>>
    {
        public async Task<PagedResponse<List<GetAllCategoryDTO>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryQuery.GetAllCategory(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });
        }
    }
}
