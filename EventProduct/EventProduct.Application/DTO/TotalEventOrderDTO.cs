using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class TotalEventOrderDTO
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public int Total { get; set; }
    }
}
