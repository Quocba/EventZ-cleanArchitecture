using MediatR;
using Payment.Application.DTO;
using Payment.Application.Interfaces;
using Payment.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Queries.GetListPaymentEvent
{
    public class GetListPaymentEventQueryHandler(IPaymentEventQuery _paymentEventQuery) : IRequestHandler<GetListPaymentEventQuery, PagedResponse<List<PaymentEventListResponse>>>
    {
        public async Task<PagedResponse<List<PaymentEventListResponse>>> Handle(GetListPaymentEventQuery request, CancellationToken cancellationToken)
        {
            return await _paymentEventQuery.GetListPaymentEvent(new PagedRequestPaymentEvent
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                EventId = request.EventId,
            });
        }
    }
}
