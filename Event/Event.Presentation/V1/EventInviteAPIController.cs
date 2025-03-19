using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.EventInvite.CheckInviteUser;
using Event.Application.Feature.EventInvite.Command;
using Event.Application.Feature.EventRegistrationLink.Commands.CheckInviteCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event")]
    [ApiController]
    public class EventInviteAPIController(ISender _sender, IConfiguration _configuration) : BaseAPIController
    {
        [HttpPost("invite-user")]
        public async Task<IActionResult> InviteUser([FromBody]InviteUserCommand command)
        {
           var result = await _sender.Send(command);
           return Success(result, "Success");
        }

        [HttpGet("confirm-invite-user")]
        public async Task<IActionResult> ConfirmInviteUser([FromQuery]CheckInviteUserCommand command)
        {
            var result = await _sender.Send(command);
            return Redirect(_configuration["RollBack:Client"]!);
        }
    }
}
