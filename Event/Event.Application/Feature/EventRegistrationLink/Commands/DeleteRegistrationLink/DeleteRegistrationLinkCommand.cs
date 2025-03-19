using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventRegistrationLink.Commands.DeleteRegistrationLink
{
    public class DeleteRegistrationLinkCommand : IRequest
    {
        public Guid RegistrationLinkID { get; set; }
        public DeleteRegistrationLinkCommand() { }
        public DeleteRegistrationLinkCommand(Guid registrationLinkID) {

            RegistrationLinkID = registrationLinkID;
        }
    }
}
