using MediatR;
using Payment.Application.DTO;
using Payment.Application.Wrappers;
using Payment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Queries.GetListPaymentEvent
{
    public class GetListPaymentEventQuery : IRequest<PagedResponse<List<PaymentEventListResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid EventId { get; set; }
    }
}
