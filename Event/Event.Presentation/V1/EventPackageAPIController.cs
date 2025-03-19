using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventPackage.AddEventPackage;
using Event.Application.Feature.EventPackage.Commands.UpdateEventPackage;
using Event.Application.Feature.EventPackage.Queries.GetAllEventPackage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event-package")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventPackageAPIController(ISender _sender) : BaseAPIController
    {
        [HttpPost("add-event-package")]
        public async Task<IActionResult> AddEventPackage([FromBody] AddEventPackageCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _sender.Send(command);
            return Success(command, "Add event package success");

        }

        [HttpGet("get-all-event-package")]
        public async Task<IActionResult> GetAllEventPackge([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetAllEventPackageQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _sender.Send(new GetAllEventPackageQuery());
            return Success(await _sender.Send(request), "Get list user successful");
        }


        [HttpPut("update-event-package")]
        public async Task<IActionResult> UpdateEventPackage([FromBody] UpdateEventPackageCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _sender.Send(command);
            return Success(command, "Update event package success");  

        }
    }
}
