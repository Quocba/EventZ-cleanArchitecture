using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class TotalRevenueProductDTO
    {
        public Guid EventID { get; set; }
        public string Title {  get; set; }
        public Decimal TotalRevenue{get; set; }
    }
}
