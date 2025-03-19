using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Contract.DTO
{
    public class SendMailInviteUserDTO
    {
        public string Email { get; set; }
        public string Subject {  get; set; }
        public string EventTItle { get; set; }
        public string Content { get; set; }
    }
}
