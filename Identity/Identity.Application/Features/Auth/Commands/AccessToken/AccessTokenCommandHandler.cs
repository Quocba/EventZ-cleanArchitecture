using Identity.Application.DTO;
using Identity.Application.Exceptions;
using Identity.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands.AccessToken
{
    public class AccessTokenCommandHandler(IUserRepository _userRepository
        , ITokenService _tokenService) : IRequestHandler<AccessTokenCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(AccessTokenCommand command, CancellationToken cancellationToken)
        {
            string accessToken = "";
            var user = await _userRepository.GetUserByRefreshToken(command.RefreshToken);

            accessToken = _tokenService.GenerateJwtToken(new UserClaim
            {
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                Roles = [.. user.UserRoles.Select(x => x.Role.Name)]
            });

            return new LoginResponse
            {
                Email = user.Email,
                UserName = user.UserName,
                AccessToken = accessToken,
                RefreshToken = command.RefreshToken
            };
        }
    }
}
