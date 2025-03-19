using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventImage.Command.DeleteEventImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event-image")]
    [ApiController]
    [ApiVersion("1.0")]

    public class EventImageAPIController(ISender _sender) : BaseAPIController
    {
        [HttpDelete("delete-event-image")]
        public async Task<IActionResult> DeleteEventImage([FromQuery] DeleteEventImageCommand command)
        {
            await _sender.Send(command);
            return Success("Delete Success", null);
        }
    }
}
