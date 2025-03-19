using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.DTO;
using Payment.Application.Wrappers;

namespace Payment.Application.Features.PaymentEvent.Queries.GetPaymentByStatus
{
    public class GetPaymentByStatusQuery : IRequest<PagedResponse<List<PaymentEventListResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Status {  get; set; }
        public GetPaymentByStatusQuery() { }
        public GetPaymentByStatusQuery(int status)
        {
            Status = status;
        }


    }
}
