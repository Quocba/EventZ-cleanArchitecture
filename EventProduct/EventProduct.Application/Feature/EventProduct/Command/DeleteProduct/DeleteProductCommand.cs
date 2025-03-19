using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventProduct.Command.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid ProductID { get; set; }

        public DeleteProductCommand() { }
        public DeleteProductCommand(Guid productID)
        {

            ProductID = productID;
        }
    }
}
