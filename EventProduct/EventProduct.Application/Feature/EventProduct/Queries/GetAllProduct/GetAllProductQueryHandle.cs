using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Interface;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Queries.GetAllProduct
{
    public class GetAllProductQueryHandle(IProductQuery _productQuery) : IRequestHandler<GetAllProductQuery, PagedResponse<List<GetAllProductDTO>>>
    {
        public async Task<PagedResponse<List<GetAllProductDTO>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await _productQuery.GetAllProduct(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            }, request.Name);
        }
    }
}
