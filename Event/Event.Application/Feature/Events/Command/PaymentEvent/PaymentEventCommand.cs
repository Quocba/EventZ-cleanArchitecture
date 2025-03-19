using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Events.Command.PaymentEvent
{
    public class PaymentEventCommand : IRequest
    {
        public Guid EventID { get; set; }
    }
}
