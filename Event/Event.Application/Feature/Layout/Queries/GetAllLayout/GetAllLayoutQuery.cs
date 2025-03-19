using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Layout.Queries.GetAllLayout
{
    public class GetAllLayoutQuery : IRequest<PagedResponse<List<LayoutResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
    }
}
