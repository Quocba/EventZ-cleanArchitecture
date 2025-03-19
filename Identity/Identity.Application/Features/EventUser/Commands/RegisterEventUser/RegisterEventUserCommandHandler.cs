using Identity.Application.Exceptions;
using Identity.Application.Handlers;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterEventUserCommandHandler(IUserRepository _userRepository
        , IRepository<UserEvent> _userEventRepository
        , IPasswordHasher _passwordHasher
        , IEmailSender _emailSender
        , IConfiguration _configuration) : IRequestHandler<RegisterEventUserCommand>
    {
        async Task IRequestHandler<RegisterEventUserCommand>.Handle(RegisterEventUserCommand command, CancellationToken cancellationToken)
        {
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

            UserEvent userEvent = new()
            {
                UserId = userId,
                EventId = command.EventId,
                EventRoleId = command.EventRoleId
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
