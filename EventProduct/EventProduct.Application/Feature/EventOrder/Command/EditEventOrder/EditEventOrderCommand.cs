using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventOrder.Command.EditEventOrder
{
    public class EditEventOrderCommand : IRequest
    {
        public Guid EventOrderID { get; set; }

        public string FullName {  get; set; }
        public string Email {  get; set; }
        public string Phone {  get; set; }

        public string Address {  get; set; }
    }
}
