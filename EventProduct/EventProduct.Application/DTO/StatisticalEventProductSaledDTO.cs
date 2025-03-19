using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class StatisticalEventProductSaledDTO
    {
        public Guid EventID {  get; set; }
        public string EventTitle {  get; set; }

        public int TotalProductSaled {  get; set; }
    }
}
