using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.DTO;
using Payment.Application.Interfaces;
using Payment.Application.Wrappers;

namespace Payment.Application.Features.PaymentEvent.Queries.GetPaymentByStatus
{
    public class GetPaymentByStatusQueryHandle(IPaymentEventQuery _paymentEventQuery)
        : IRequestHandler<GetPaymentByStatusQuery, PagedResponse<List<PaymentEventListResponse>>>
    {
        public async Task<PagedResponse<List<PaymentEventListResponse>>> Handle(GetPaymentByStatusQuery request, CancellationToken cancellationToken)
        {
            var result = await _paymentEventQuery.GetListPaymentWithStatus(new PagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            }, request.Status);

            return result;
        }
    }
}
