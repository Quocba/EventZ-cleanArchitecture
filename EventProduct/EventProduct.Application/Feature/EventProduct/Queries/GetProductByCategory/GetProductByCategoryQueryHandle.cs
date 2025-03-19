using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Interface;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Queries.GetEventByCategory
{
    public class GetProductByCategoryQueryHandle(IProductQuery _productQuery) : IRequestHandler<GetProductByCategoryQuery, PagedResponse<List<GetAllProductDTO>>>
    {
        public async Task<PagedResponse<List<GetAllProductDTO>>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _productQuery.GetProductByCategory(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,

            },request.CategoryID,request.Name);
        }
    }
}
