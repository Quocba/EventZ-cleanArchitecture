using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventPackage.Queries.GetRegistrationByEventPackage;
using Event.Application.Feature.EventPackageRegistration.Commands.EditEventPackageRegistrationStatus;
using Event.Application.Feature.EventPackageRegistration.Queries.GetAllEventPackageRegistration;
using Event.Application.Feature.EventPackageRegistration.Queries.GetEventPackageRegistrationDetail;
using Event.Application.Feature.EventPackageRegistration.RegistrationEventPackge;
using Event.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event-package-registration")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventPackageRegistrationAPIController(ISender _sender) : BaseAPIController
    {
        [HttpPost("registration-event-package")]
        public async Task<IActionResult> RegistrationEventPackage([FromBody] RegistrationEventPackageCommand command)
        {
                await _sender.Send(command);
                return Success(command, "Registration Success");
        }

        [HttpGet("get-all-event-package-registration")]
        public async Task<IActionResult> GetAllEventPackageRegistration([FromQuery] string? status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize =10)
        {
            var request = new GetAllEventPackageRegistrationQuery(status) { PageNumber = pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(request), "Success");
        }

        [HttpPut("edit-status-event-package-registration")]
        public async Task<IActionResult> EditStatusEventPackageRegistration([FromBody] EditEventPackageRegistrationStatusCommand command)
        {

            await _sender.Send(command);
            return Success(command, "Update Success");

        }

        [HttpGet("{eventPackageRegistrationID}")]
        public async Task<IActionResult> GetEventRegistrationDetail(Guid eventPackageRegistrationID)
        {
            var query = new GetEventRegistrationDetailQuery(eventPackageRegistrationID);
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpGet("get-registration-by-event-package")]
        public async Task<IActionResult> GetRegistrationByEventPacakge([FromQuery]Guid eventPackageID, [FromQuery]int pageNumber = 1, int pageSize = 10)
        {
            var request = new GetRegistrationByEventPackageQuery(eventPackageID) { PageNumber=pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(request), "Success");
        }
    }
}
