using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class GetAllEventDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid UserID { get; set; }
        public Guid EventTypeID { get; set; }
    }
}
