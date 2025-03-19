using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Queries.GetAllProduct
{
    public class GetAllProductQuery : IRequest<PagedResponse<List<GetAllProductDTO>>>
    {

        public int PageNumber = 1;
        public int PageSize = 10;
        public string Name {  get; set; }
        public GetAllProductQuery() { }

        public GetAllProductQuery(string name)
        {
            Name = name;
        }
        
    }
}
