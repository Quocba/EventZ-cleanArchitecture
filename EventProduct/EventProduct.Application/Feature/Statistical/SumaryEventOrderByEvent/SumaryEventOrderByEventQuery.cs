using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using MediatR;

namespace EventProduct.Application.Feature.Statistical.EventOrder
{
    public class SumaryEventOrderByEventQuery : IRequest<TotalEventOrderDTO>
    {
        public Guid eventID { get; set; }
        public SumaryEventOrderByEventQuery() { }
        public SumaryEventOrderByEventQuery(Guid eventID)
        {
            this.eventID = eventID;
        }
    }
}
