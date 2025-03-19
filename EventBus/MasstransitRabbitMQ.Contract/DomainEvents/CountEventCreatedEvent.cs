using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Contract.DomainEvents
{
    public class CountEventCreatedEvent
    {
        public Guid UserId { get; set; }
    }
}
