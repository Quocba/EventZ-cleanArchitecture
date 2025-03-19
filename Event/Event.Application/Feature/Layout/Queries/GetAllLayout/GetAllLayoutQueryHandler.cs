using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Layout.Queries.GetAllLayout
{
    public class GetAllLayoutQueryHandler(ILayoutQuery _layoutQuery) : IRequestHandler<GetAllLayoutQuery, PagedResponse<List<LayoutResponse>>>
    {
        public async Task<PagedResponse<List<LayoutResponse>>> Handle(GetAllLayoutQuery request, CancellationToken cancellationToken)
        {
            return await _layoutQuery.GetAllLayout(new PagedRequestWithSearch
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Search = request.Search
            });
        }
    }
}
