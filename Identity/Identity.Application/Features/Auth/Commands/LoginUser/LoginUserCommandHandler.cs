using Identity.Application.DTO;
using Identity.Application.Exceptions;
using Identity.Application.Handlers;
using Identity.Application.Interfaces;
using Identity.Domain.Shares;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler(IUserRepository _userRepository
        , IPasswordHasher _passwordHasher
        , ITokenService _tokenService) : IRequestHandler<LoginUserCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            string accessToken = "";
            string refreshToken = "";
            var user = await _userRepository.GetByUsernameAsync(command.Username);

            if (!_passwordHasher.VerifyPassword(command.Password, user.PasswordHash)) throw new InvalidCredentialsException("Invalid credentials");

            if (!user.IsEmailConfirmed) throw new InvalidCredentialsException("Please confirm email");

            accessToken = _tokenService.GenerateJwtToken(new UserClaim
            {
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                Roles = [.. user.UserRoles.Select(x => x.Role.Name)]
            });
            refreshToken = _tokenService.GenerateRefreshToken(64);

            await _userRepository.SaveRefreshToken(user.Id, refreshToken, DateUtility.GetCurrentDateTime().AddDays(1));
            await _userRepository.SaveAsync();

            return new LoginResponse
            {
                Email = user.Email,
                UserName = user.UserName,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
