using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.Events.Queries.GetAllEvents
{
    public class GetAllEventQuery : IRequest<PagedResponse<List<GetAllEventDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Status {  get; set; }
        public GetAllEventQuery(string? status)
        {
            Status = status;
        }
    }
}
