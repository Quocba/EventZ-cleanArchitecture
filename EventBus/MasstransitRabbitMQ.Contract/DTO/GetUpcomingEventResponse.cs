using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Contract.DTO
{
    public class GetUpcomingEventResponse
    {
        public List<UpComingEventDTO> Events { get; set; } = new();
    }
}
