using Event.Application.Interface;
using Event.Application.Interfaces;
using Event.Domain.Shares;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.EventRegistrationLink.Commands.CheckInviteCode
{
    public class CheckInviteCodeCommandHandle(IEventRegistrationLinkRepository _eventRegistrationLinkRepository)
        : IRequestHandler<CheckInviteCodeCommand, Guid>
    {
        public async Task<Guid> Handle(CheckInviteCodeCommand request, CancellationToken cancellationToken)
        {
            var registerLink = await _eventRegistrationLinkRepository.GetByCodeAsync(request.InviteCode);

            if (registerLink != null && registerLink.EndDate >= DateUtility.GetCurrentDateTime() && registerLink.StartDate <= DateUtility.GetCurrentDateTime())
            {
                return registerLink.EventID;
            }

            return Guid.Empty;
        }

    }
}
