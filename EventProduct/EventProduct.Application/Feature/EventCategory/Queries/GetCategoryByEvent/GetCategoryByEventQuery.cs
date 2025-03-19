using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Queries.GetEventByCategory
{
    public class GetCategoryByEventQuery : IRequest<PagedResponse<List<GetAllCategoryDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public Guid EventID {  get; set; }
        public GetCategoryByEventQuery(Guid evetnID) {

            EventID = evetnID;
        }

    }
}
