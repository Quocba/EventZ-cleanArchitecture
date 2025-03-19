using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Feature.EventCategory.Queries.GetAllCategory;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Queries.GetEventByCategory
{
    public class GetProductByCategoryQuery : IRequest<PagedResponse<List<GetAllProductDTO>>>
    {
        public int PageNumber = 1;
        public int PageSize = 10;
        [Required]
        public Guid CategoryID { get; set; }
        public string Name {  get; set; }
        public GetProductByCategoryQuery() { }
        public GetProductByCategoryQuery(Guid categoryID, string name) { 

            CategoryID = categoryID;
            Name = name;
        }

    }
}
