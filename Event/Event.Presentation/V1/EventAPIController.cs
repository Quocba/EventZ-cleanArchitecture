using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventPackage.AddEventPackage;
using Event.Application.Feature.EventPackage.Commands.UpdateEventPackage;
using Event.Application.Feature.EventPackage.Queries.GetAllEventPackage;
using Event.Application.Feature.EventPackageRegistration.Commands.EditEventPackageRegistrationStatus;
using Event.Application.Feature.EventPackageRegistration.RegistrationEventPackge;
using Event.Application.Feature.Events;
using Event.Application.Feature.Events.Command.ConfirmEvent;
using Event.Application.Feature.Events.Command.CreateEvent;
using Event.Application.Feature.Events.Command.EditEvent;
using Event.Application.Feature.Events.Queries.GetAllEvents;
using Event.Application.Feature.Events.Queries.GetEventByUser;
using Event.Application.Feature.Events.Queries.GetEventDetail;
using Event.Application.Feature.Events.Queries.GetListEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventAPIController(ISender _sender) : BaseAPIController
    {
     
        [HttpPost("create-event")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _sender.Send(command);
            return Ok( new { Message = "Create Event Success", Data = id });       
        }

        [HttpGet("get-all-event")]
        public async Task<IActionResult> GetAllEvent([FromQuery]string? status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetAllEventQuery(status){ PageNumber = pageNumber, PageSize = pageSize};
            return Success(await _sender.Send(request), "Get list event successful");
        }

        [HttpPut("confirm-event")]
        public async Task<IActionResult> ConfirmEvent([FromBody] ConfirmEventCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             await _sender.Send(command);
            return Success(command, "Success");
        }

        [HttpPut("edit-event")]
        public async Task<IActionResult> EditEvent([FromBody]EditEventCommand command) {

            await _sender.Send(command);
            return Success(command, "Success");
        }

        [HttpGet("get-event-by-user")]
        public async Task<IActionResult> GetEventByUser([FromQuery] Guid userID, [FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var request = new GetEventByUserQuery(userID) { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _sender.Send(request);
            return Success(result,"Success");
        }

        [HttpGet("get-event-by-user-token")]
        public async Task<IActionResult> GetEventByUserToken([FromHeader(Name = "X-User-Id")] Guid userID, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetEventByUserQuery(userID) { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _sender.Send(request);
            return Success(result, "Success");
        }

        [HttpGet("get-event-detail")]
        public async Task<IActionResult> GetEventDetail([FromQuery] GetEventDetailQuery query)
        {
            var result = await _sender.Send(query);
            return Success(result, "Success");
        }

        [HttpGet("get-upcoming-event")]
        public async Task<IActionResult> GetFullEvent([FromQuery]GetUpcomingEventQuery query)
        {
            return Success(await _sender.Send(query), "Success");
        }
    }
}
