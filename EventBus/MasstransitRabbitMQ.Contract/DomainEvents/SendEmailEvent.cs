using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Contract.DomainEvents
{
    public class SendEmailEvent
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string HtmlMessage { get; set; }
    }
}
