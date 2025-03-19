using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventProductOrder.Command.EditQuantityEventOrderProduct
{
    public class EditEventProductOrderCommand :  IRequest
    {
        [JsonIgnore]
        public Guid EventProductOrderID { get; set; }
        public int Quantity { get; set; }
    }
}
