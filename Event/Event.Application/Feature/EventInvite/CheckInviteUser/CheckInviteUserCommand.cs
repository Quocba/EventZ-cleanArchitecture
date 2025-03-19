using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventInvite.CheckInviteUser
{
    public class CheckInviteUserCommand : IRequest<string>
    {
        public string Token { get; set; }
        public Guid EventID { get; set; }

        public CheckInviteUserCommand() { }
        public CheckInviteUserCommand(string token, Guid eventID)
        {
            Token = token;
            EventID = eventID;
        }
    }
}
