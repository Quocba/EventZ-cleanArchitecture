using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Domain.Shares;
using MediatR;

namespace EventProduct.Application.Feature.EventOrder.Command.CreateEventOrder
{
    public class CreateEventOrderCommand : IRequest<Guid>
    {
        public double TotalPrice { get; set; } = 0;
        public DateTime CreateAt { get; set; } = DateUtility.GetCurrentDateTime();
        public DateTime UpdateAt { get; set; }

        public Guid EventID { get; set; }
        public Guid UserID { get; set; }

        public string FullName {  get; set; }
        public string Email {  get; set; }

        public string Phone {  get; set; }

        public string Address {  get; set; }

        public Guid PaymentHistoryID { get; set; }
    }
}
