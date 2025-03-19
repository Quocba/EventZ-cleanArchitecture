using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Event.Application.Feature.EventInvite.CheckInviteUser
{
    public class CheckInviteUserCommandHandle(IRepository<Domain.Entities.EventInvite> _eventInviteRepository, IConfiguration _configuration)
        : IRequestHandler<CheckInviteUserCommand, string>
    {
        public async Task<string> Handle(CheckInviteUserCommand request, CancellationToken cancellationToken)
        {
            var email = GetEmailFromToken(request.Token);
            if (string.IsNullOrEmpty(email))
                return "Invalid Token";

            var invite = await _eventInviteRepository
                .FindWithInclude()
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if (invite == null)
                return "Invite Not Found";

            invite.IsConfirm = true;
            await _eventInviteRepository.UpdateAsync(invite);
            await _eventInviteRepository.SaveAsync();
            return "Confirm Success";

        }

        private string GetEmailFromToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var principal = tokenHandler.ValidateToken(token, validations, out var validatedToken);
                return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
