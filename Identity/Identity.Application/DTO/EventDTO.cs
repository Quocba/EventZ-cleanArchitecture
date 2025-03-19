using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.DTO
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string AdditionalInfo { get; set; }
        public int NumberOfGuest { get; set; }
        public bool IsActive { get; set; }
        public bool IsOpenLayout { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
    }
}
