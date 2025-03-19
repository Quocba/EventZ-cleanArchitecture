using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventRegistrationLink.Commands.CreateRegistrationLink
{
    public class CreateRegistrationLinkComand : IRequest
    {
        public string Code {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid EventID { get; set; }
    }
}
