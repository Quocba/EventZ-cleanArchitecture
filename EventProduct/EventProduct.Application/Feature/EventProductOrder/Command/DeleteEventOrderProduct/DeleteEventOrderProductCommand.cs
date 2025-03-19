using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventProductOrder.Command.DeleteEventOrderProduct
{
    public class DeleteEventOrderProductCommand : IRequest
    {
        public Guid EventProductOrderID { get; set; }

    }
}
