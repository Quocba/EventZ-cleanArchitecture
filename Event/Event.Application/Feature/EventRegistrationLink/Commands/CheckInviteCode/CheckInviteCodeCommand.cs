using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.EventRegistrationLink.Commands.CheckInviteCode
{
    public class CheckInviteCodeCommand : IRequest<Guid>
    {
        public string InviteCode { get; set; }
    }
}
