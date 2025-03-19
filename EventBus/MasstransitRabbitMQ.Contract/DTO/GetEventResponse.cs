using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Contract.DTO
{
    public class GetEventResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public bool IsApproved { get; set; }
    }
}
