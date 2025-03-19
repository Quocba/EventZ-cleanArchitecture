using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.EventDocuments.Queries.GetDocumentByEvent
{
    public class GetDocumentByEventQueryHandle(IEventDocumentQuery _eventDocumentQuery) : IRequestHandler<GetDocumentByEventQuery,PagedResponse<List<GetDocumentByEventDTO>>>
    {
        public async Task<PagedResponse<List<GetDocumentByEventDTO>>> Handle(GetDocumentByEventQuery request, CancellationToken cancellationToken)
        {
            return await _eventDocumentQuery.GetDocumentsByEvent(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            }, request.EventID);
        }
    }
}
