using Identity.Application.Exceptions;
using Identity.Application.Handlers;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IUserRepository _userRepository
        , IRepository<UserRole> _userRoleRepository
        , IPasswordHasher _passwordHasher
        , IRequestClient<SendEmailEvent> _requestSendEmailEvent
        , IConfiguration _configuration) : IRequestHandler<RegisterUserCommand>
    {
        async Task IRequestHandler<RegisterUserCommand>.Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(command.UserName);

            if (existingUser != null) throw new InvalidCredentialsException("Username already exists");

            var existingEmail = await _userRepository.GetByEmailAsync(command.Email);

            if (existingEmail != null) throw new InvalidCredentialsException("Email already exists");

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
                Phone = command.Phone
            };

            await _userRepository.AddAsync(user);

            var userRole = new UserRole
            {
                UserID = userId,
                RoleID = Guid.Parse("80AE6925-A266-455C-9D0C-DC4CD3205BA4")
            };

            await _userRoleRepository.AddAsync(userRole);

            string code = await _userRepository.GenerateCodeConfirmEmail(userId);

            await _userRepository.SaveAsync();

            string url = _configuration["Base:Url"] ?? string.Empty;
            string content = url + "auth/confirm-email?userId=" + userId + "&code=" + code;

            var response = await _requestSendEmailEvent.GetResponse<SendEmailResponse>
                (new SendEmailEvent { Email = user.Email, Subject = "Confirm email", HtmlMessage = content }, cancellationToken);
        }
    }
}
