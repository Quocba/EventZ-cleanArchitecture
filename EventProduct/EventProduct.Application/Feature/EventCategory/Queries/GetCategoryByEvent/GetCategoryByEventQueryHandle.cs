using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Interface;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Queries.GetEventByCategory
{
    public class GetCategoryByEventQueryHandle(ICategoryQuery _categoryQuery) : IRequestHandler<GetCategoryByEventQuery, PagedResponse<List<GetAllCategoryDTO>>>
    {
        public async Task<PagedResponse<List<GetAllCategoryDTO>>> Handle(GetCategoryByEventQuery request, CancellationToken cancellationToken)
        {
            return await _categoryQuery.GetCategoryByEvent(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            }, request.EventID);
        }
    }
}
