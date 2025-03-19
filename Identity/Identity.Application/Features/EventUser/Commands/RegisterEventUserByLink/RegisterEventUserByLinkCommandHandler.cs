using Identity.Application.Exceptions;
using Identity.Application.Features.Auth.Commands.RegisterUser;
using Identity.Application.Handlers;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using MailKit.Search;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.EventUser.Commands.RegisterEventUserByLink
{
    public class RegisterEventUserCommandByLinkHandler(IUserRepository _userRepository
        , IRepository<Domain.Entities.UserEvent> _userEventRepository
        , IPasswordHasher _passwordHasher
        , IEmailSender _emailSender
        , IConfiguration _configuration
        , IRequestClient<CheckInviteCodeEvent> _requestCheckInvalidCodeEvent) : IRequestHandler<RegisterEventUserByLinkCommand>
    {
        async Task IRequestHandler<RegisterEventUserByLinkCommand>.Handle(RegisterEventUserByLinkCommand command, CancellationToken cancellationToken)
        {
            var response = await _requestCheckInvalidCodeEvent.GetResponse<CheckInviteCodeResponse>(
                new CheckInviteCodeEvent { InviteCode = command.RegistrationCode },
                cancellationToken
            );

            if (response.Message.EventId == Guid.Empty)
            {
                throw new ValidationException("Invalid invite code");
            }

            var existingUser = await _userRepository.GetByUsernameAsync(command.UserName);

            if (existingUser != null) throw new InvalidCredentialsException("Username already exists");

            Guid userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = _passwordHasher.HashPassword(command.PasswordHash),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Avatar = command.Avatar,
                Gender = command.Gender,
                Phone = command.Phone,
                CreateById = command.CreateById
            };

            await _userRepository.AddAsync(user);

            Domain.Entities.UserEvent userEvent = new()
            {
                UserId = userId,
                EventId = command.EventId,
                EventRoleId = new Guid("1348A93D-B890-434F-9D55-829E5E8F3A8B")
            };

            await _userEventRepository.AddAsync(userEvent);

            string code = await _userRepository.GenerateCodeConfirmEmail(userId);

            await _userRepository.SaveAsync();

            string url = _configuration["Base:Url"] ?? string.Empty;
            string content = url + "auth/confirm-email?userId=" + userId + "&code=" + code;

            await _emailSender.SendEmailAsync(command.Email, "Confirm email", content);
        }
    }
}
