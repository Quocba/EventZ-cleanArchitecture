using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventDocuments.Command.AddEventDocument;
using Event.Application.Feature.EventDocuments.Command.DeleteEventDocument;
using Event.Application.Feature.EventDocuments.Command.EditEventDocument;
using Event.Application.Feature.EventDocuments.Queries.GetDocumentByEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event-documents")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventDocumentsAPIController(ISender _sender) : BaseAPIController
    {
        [HttpPost("add-event-document")]
        public async Task<IActionResult> AddEventDocument([FromBody] AddEventDocumentsCommand command)
        {
            {
                await _sender.Send(command);
                return Success(command, "Success");
            }
        }

        [HttpPut("edit-event-document")]
        public async Task<IActionResult> EditEventDocument([FromBody] EditEventDocumentsCommand command)
        {
            await _sender.Send(command);
            return Success(command, "Success");
        }

        [HttpDelete("delete-event-document")]
        public async Task<IActionResult> DeleteEventDocument([FromQuery]DeleteDocumentCommand command)
        {
            await _sender.Send(command);
            return Success("Delete document success", null);
        }

        [HttpGet("get-document-by-event")]
        public async Task<IActionResult> GetDocumentByEvent([FromQuery]Guid eventID, [FromQuery]int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetDocumentByEventQuery(eventID) { PageNumber = pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(request), "Success");
        }
    }
}
