using Identity.Application.Exceptions;
using Identity.Application.Features.Auth.Commands.ResendConfirmEmail;
using Identity.Application.Handlers;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.ResendEmail
{
    public class ResendConfirmEmailCommandHandler(IUserRepository _userRepository
        , IRequestClient<SendEmailEvent> _requestSendEmailEvent
        , IConfiguration _configuration) : IRequestHandler<ResendConfirmEmailCommand>
    {
        async Task IRequestHandler<ResendConfirmEmailCommand>.Handle(ResendConfirmEmailCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(command.Email);

            string code = await _userRepository.GenerateCodeConfirmEmail(existingUser.Id);

            await _userRepository.SaveAsync();

            string url = _configuration["Base:Url"] ?? string.Empty;
            string content = url + "auth/confirm-email?userId=" + existingUser.Id + "&code=" + code;

            await _requestSendEmailEvent.GetResponse<SendEmailResponse>
                (new SendEmailEvent { Email = existingUser.Email, Subject = "Confirm email", HtmlMessage = content }, cancellationToken);
        }
    }
}
