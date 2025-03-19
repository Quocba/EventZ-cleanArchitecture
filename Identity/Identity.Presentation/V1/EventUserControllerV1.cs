using Asp.Versioning;
using Base.API;
using Identity.Application.Features.Auth.Commands.AccessToken;
using Identity.Application.Features.Auth.Commands.ChangeDeleteUser;
using Identity.Application.Features.Auth.Commands.ChangePassword;
using Identity.Application.Features.Auth.Commands.ConfirmEmail;
using Identity.Application.Features.Auth.Commands.ForgotPassword;
using Identity.Application.Features.Auth.Commands.LoginUser;
using Identity.Application.Features.Auth.Commands.RegisterUser;
using Identity.Application.Features.Auth.Commands.ResendConfirmEmail;
using Identity.Application.Features.Auth.Commands.ResetPassword;
using Identity.Application.Features.Auth.Queries.GetMe;
using Identity.Application.Features.EventUser.Queries.GetEventUser;
using Identity.Application.Features.EventUser.Queries.GetEventUsers;
using Identity.Application.Features.Users.Commands.UpdateAccount;
using Identity.Application.Features.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event-user")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventControllerV1(ISender _sender) : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetEventUsersQuery request)
        {
            return Success(await _sender.Send(request), "Get list event user successful");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Success(await _sender.Send(new GetEventUserQuery { Id = id }), "Get event user by " + id);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterEventUserCommand command)
        {
            await _sender.Send(command);
            return Success(null, "User registered successfully.");
        }

        [HttpPost("register-by-link")]
        public async Task<IActionResult> RegisterByLink([FromBody] RegisterEventUserByLinkCommand command)
        {
            await _sender.Send(command);
            return Success(null, "User registered successfully.");
        }
    }
}
