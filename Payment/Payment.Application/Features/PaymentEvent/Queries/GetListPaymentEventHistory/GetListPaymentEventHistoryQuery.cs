using MediatR;
using Payment.Application.DTO;
using Payment.Application.Wrappers;
using Payment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Queries.GetListPaymentEventHistory
{
    public class GetListPaymentEventHistoryQuery : IRequest<PagedResponse<List<PaymentEventListResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid UserId { get; set; }
    }
}
