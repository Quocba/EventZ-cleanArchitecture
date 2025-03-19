using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Queries.GetHandleByTimeLine
{
    public class GetHandleByTimeLineQuery : IRequest<PagedResponse<List<GetHandleByEventTimeLineDTO>>>
    {
        public int PageNumber = 1;
        public int PageSize = 10;
    }
}
