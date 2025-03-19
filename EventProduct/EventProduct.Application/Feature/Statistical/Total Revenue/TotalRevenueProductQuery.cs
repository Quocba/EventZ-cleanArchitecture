using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using MediatR;

namespace EventProduct.Application.Feature.Statistical.Total_Revenue
{
    public class TotalRevenueProductQuery : IRequest<TotalRevenueProductDTO>
    {
        public Guid EventID { get; set; }
        public TotalRevenueProductQuery() { }
        public TotalRevenueProductQuery(Guid eventID)
        {
            EventID = eventID;
        }
    }
}
