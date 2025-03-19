using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventProductOrder.Command.CreateEventOrderProduct
{
    public class CreateEventProductOrderCommand : IRequest<Guid>
    {
        public Guid ProductID { get; set; }
        public Guid EventOrderID { get; set; }
        public int Quantity { get; set; }

    }
}
