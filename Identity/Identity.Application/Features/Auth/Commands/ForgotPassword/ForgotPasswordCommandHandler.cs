using Identity.Application.Exceptions;
using Identity.Application.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler(IUserRepository _userRepository
        , IRequestClient<SendEmailEvent> _requestSendEmailEvent
        , IConfiguration _configuration) : IRequestHandler<ForgotPasswordCommand>
    {
        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            string code = await _userRepository.GenerateCodeConfirmEmail(existingUser.Id);

            await _userRepository.SaveAsync();

            string url = _configuration["Base:UrlClient"] ?? string.Empty;
            string content = url + "/api/auth/reset-password?userId=" + existingUser.Id + "&code=" + code;

            await _requestSendEmailEvent.GetResponse<SendEmailResponse>(
                new SendEmailEvent { Email = existingUser.Email, Subject = "Forgot password", HtmlMessage = content },
                cancellationToken
            );
        }
    }
}
