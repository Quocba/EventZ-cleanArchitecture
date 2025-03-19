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
using Identity.Application.Features.Users.Commands.UpdateAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.Presentation.V1
{
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthControllerV1(ISender _sender) : BaseAPIController
    {
        [HttpGet("get-me")]
        public async Task<IActionResult> GetMe([FromHeader(Name = "X-User-Id")] Guid userId)
        {
            var user = await _sender.Send(new GetMeQuery { Id = userId });
            return Success(user, "Get user successfully.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await _sender.Send(command);
            return Success(null, "User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var response = await _sender.Send(command);
            if (response == null) return Unauthorized();
            return Success(response, "Login successful.");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] Guid userId, [FromQuery] string code)
        {
            var response = await _sender.Send(new ConfirmEmailCommand { Code = code, UserId = userId });
            return Success(response, "Email confirmed successfully.");
        }

        [HttpPost("resend-confirm")]
        public async Task<IActionResult> ReSendConfirmEmail([FromBody] ResendConfirmEmailCommand command)
        {
            await _sender.Send(command);
            return Success(null, "Resend confirmation email successful.");
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromHeader(Name = "X-User-Id")] Guid userId, [FromBody] ChangePasswordCommand command)
        {
            command.UserId = userId;
            await _sender.Send(command);
            return Success(null, "Password changed successfully.");
        }

        [HttpPut("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _sender.Send(command);
            return Success(null, "Password reset email sent.");
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _sender.Send(command);
            return Success(null, "Password reset successful.");
        }

        [HttpPut("access-token")]
        public async Task<IActionResult> AccessToken([FromBody] AccessTokenCommand command)
        {
            var response = await _sender.Send(command);
            return Success(response, "Access token refreshed successfully.");
        }

        [HttpPut("profile")]
        public async Task<IActionResult> Update([FromHeader(Name = "X-User-Id")] Guid userId, [FromBody] UpdateUserProfileCommand request)
        {
            request.Id = userId;
            await _sender.Send(request);
            return Success(userId, "Update profile successful");
        }

        [HttpPut("block")]
        public async Task<IActionResult> Block([FromQuery] Guid id)
        {
            ChangeDeletedUserCommand command = new() { IsDeleted = true, UserId = id };
            await _sender.Send(command);
            return Success(id, "User blocked successfully.");
        }

        [HttpPut("unblock")]
        public async Task<IActionResult> UnBlock([FromQuery] Guid id)
        {
            ChangeDeletedUserCommand command = new() { IsDeleted = false, UserId = id };
            await _sender.Send(command);
            return Success(id, "User unblocked successfully.");
        }
    }
}
