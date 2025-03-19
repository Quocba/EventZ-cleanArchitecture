using Identity.Application.Exceptions;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Users.Commands.UpdateAccount
{
    public class UpdateUserProfileCommandHandler(IUserRepository _userRepository) : IRequestHandler<UpdateUserProfileCommand>
    {
        public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("User not found");

            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;
            user.Avatar = request.Avatar ?? user.Avatar;
            user.Gender = request.Gender ?? user.Gender;


            await _userRepository.UpdateAsync(user);

            await _userRepository.SaveAsync();
        }
    }
}
