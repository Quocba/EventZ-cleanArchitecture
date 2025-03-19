using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventTimeLine.Command.AddEventTimeLine;
using Event.Application.Feature.EventTimeLine.Command.DeleteEventTimeLine;
using Event.Application.Feature.EventTimeLine.Command.EditEventTimeLine;
using Event.Application.Feature.EventTimeLine.Command.SendMailTimeLine;
using Event.Application.Feature.EventTimeLine.Queries.GetHandleByTimeLine;
using Event.Application.Feature.EventTimeLine.Queries.GetTimeLineByEvent;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{
    [Microsoft.AspNetCore.Mvc.Route("api/v{version:apiVersion}/event-time-line")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventTimeLineAPIController(ISender _sender) : BaseAPIController
    {
        [HttpPost("add-event-time-line")]
        public async Task<IActionResult> AddEventTimeLine([FromBody]AddEventTimeLineCommand command)
        {   
            return Success(await _sender.Send(command), "Add event time line success");
        }

        [HttpPut("edit-event-time-line")]
        public async Task<IActionResult> EditEventTimeLine([FromBody] EditEventTimeLineCommand command)
        {
            await _sender.Send(command);
            return Success(command, "Update event time line success");
        }

        [HttpDelete("delete-event-time-line")]
        public async Task<IActionResult> DeleteEventTimeLine([FromQuery] DeleteEventTimeLineCommand command)
        {
            await _sender.Send(command);
            return Success(null,"Delete event time line success");
        }

        [HttpGet("get-time-line-by-event")]
        public async Task<IActionResult> GetTimeLineByEvent([FromQuery]String status, [FromQuery] Guid eventID, [FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 10)
        {
            var request = new GetTimeLineByEventQuery(eventID, status) { PageNumber = pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(request), "Success");
        }

        [HttpPost("send-mail")]
        public async Task<IActionResult> SendMail([FromQuery]SendMailTimeLineCommand command)
        {
           await _sender.Send(command);
            return Success("", "Send Mail Success");
        }

        [HttpGet("get-all-handleby-event-time-line")]
        public async Task<IActionResult> GetAllHandleByEventTimeLine([FromQuery] int pageNumber = 1, [FromQuery]int pageSize = 10)
        {
            var request = new GetHandleByTimeLineQuery { PageNumber = pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(request), "Success");
        }
    }
}
