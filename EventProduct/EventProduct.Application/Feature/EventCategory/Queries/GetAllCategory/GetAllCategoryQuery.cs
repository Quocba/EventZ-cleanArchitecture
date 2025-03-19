using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Queries.GetAllCategory
{
    public class GetAllCategoryQuery : IRequest<PagedResponse<List<GetAllCategoryDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
