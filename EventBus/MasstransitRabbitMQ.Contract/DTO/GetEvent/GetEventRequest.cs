using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Contract.DTO.GetEvent
{
    public class GetEventRequest
    {
        public Guid eventID {  get; set; }
    }
}
