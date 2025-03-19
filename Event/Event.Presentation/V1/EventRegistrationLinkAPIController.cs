using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventRegistrationLink.Commands.CreateRegistrationLink;
using Event.Application.Feature.EventRegistrationLink.Commands.DeleteRegistrationLink;
using Event.Application.Feature.EventRegistrationLink.Queries.GetRegistrationLinkByEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event-registration-link")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventRegistrationLinkAPIController(ISender _sender) : BaseAPIController
    {
        [HttpPost("create-registration-link")]
        public async Task<IActionResult> CreateRegistrationLink([FromBody] CreateRegistrationLinkComand comand)
        {
            await _sender.Send(comand);
            return Success(comand, "Success");
        }

        [HttpDelete("delete-registration-link")]
        public async Task<IActionResult> DeleteRegistrationLink([FromQuery] DeleteRegistrationLinkCommand command) { 
        
            await _sender.Send(command);
            return Success(null,"Delete Success");
        }

        [HttpGet("get-registration-link-by-event")]
        public async Task<IActionResult> GetRegistrationLinkByEvent([FromQuery]GetRegistrationLinkByEventQuery query)
        {
            var result = await _sender.Send(query);
            return Success(result,"Success");
        }
        
    }
}
