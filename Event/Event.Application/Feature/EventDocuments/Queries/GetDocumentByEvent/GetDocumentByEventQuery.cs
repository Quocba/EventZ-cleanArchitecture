using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.EventDocuments.Queries.GetDocumentByEvent
{
    public class GetDocumentByEventQuery : IRequest<PagedResponse<List<GetDocumentByEventDTO>>>
    {
       public int PageNumber { get; set; } = 1;
       public int PageSize { get; set; } = 10;
       public Guid EventID {  get; set; }

        public GetDocumentByEventQuery() { }
        public GetDocumentByEventQuery(Guid eventID) {
            
            EventID = eventID;
        }
    }
}
