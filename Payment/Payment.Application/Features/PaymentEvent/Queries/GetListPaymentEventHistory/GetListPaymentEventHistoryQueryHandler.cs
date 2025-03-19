using MediatR;
using Payment.Application.DTO;
using Payment.Application.Interfaces;
using Payment.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Queries.GetListPaymentEventHistory
{
    public class GetListPaymentEventHistoryQueryHandler(IPaymentEventQuery _paymentEventQuery) : IRequestHandler<GetListPaymentEventHistoryQuery, PagedResponse<List<PaymentEventListResponse>>>
    {
        public async Task<PagedResponse<List<PaymentEventListResponse>>> Handle(GetListPaymentEventHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _paymentEventQuery.GetListPaymentEventHistory(new PagedRequestPaymentEventHistory
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                UserId = request.UserId,
            });
        }
    }
}
