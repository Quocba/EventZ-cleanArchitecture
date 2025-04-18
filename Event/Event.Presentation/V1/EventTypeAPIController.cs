using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventType;
using Event.Application.Feature.EventType.Queries.GetAllEventType;
using Event.Application.Feature.EventType.Queries.GetByID;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{

    [Route("api/v{version:apiVersion}/event-type")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventTypeAPIController(ISender _sender) : BaseAPIController
    {
        [HttpGet("get-all-event-type")]
        public async Task<IActionResult> GetAllEventType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetAllEventTypeQuery {PageNumber = pageNumber, PageSize = pageSize};
            return Success(await _sender.Send(request), "Get list event type successful");
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByID([FromQuery] GetByIDQuery query)
        {

            return Success(await _sender.Send(new GetByIDQuery { EventTypeID = query.EventTypeID }), "Success");
        }
    }
}
