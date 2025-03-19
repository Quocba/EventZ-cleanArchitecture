using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class EventOrderDTO
    {
        public Guid EventOderID { get; set; }
        public double TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; } 
        public string Email { get; set; }
        public string FullName {  get; set; }
        public string Phone {  get; set; }
        public string Address {  get; set; }
        public Guid EventID { get; set; }

        public Guid UserID { get; set; }

        public Guid PaymentHistoryID { get; set; }
    }
}
