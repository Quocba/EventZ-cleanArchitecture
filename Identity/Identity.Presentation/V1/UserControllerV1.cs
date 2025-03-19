using Asp.Versioning;
using Base.API;
using Identity.Application.Features.Auth.Commands.ChangeDeleteUser;
using Identity.Application.Features.Users.Commands.UpdateAccount;
using Identity.Application.Features.Users.Queries.GetUser;
using Identity.Application.Features.Users.Queries.GetUsers;
using Identity.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.V1
{
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserControllerV1(ISender _sender) : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetUsersQuery request)
        {
            return Success(await _sender.Send(request), "Get list user successful");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Success(await _sender.Send(new GetUserQuery { Id = id }), "Get user by " + id);
        }
    }
}
