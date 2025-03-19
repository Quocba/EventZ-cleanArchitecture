using Payment.Application.DTO;
using Payment.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Interfaces
{
    public interface IPaymentEventQuery
    {
        Task<PagedResponse<List<PaymentEventListResponse>>> GetListPaymentEvent(PagedRequestPaymentEvent request);
        Task<PagedResponse<List<PaymentEventListResponse>>> GetListPaymentEventHistory(PagedRequestPaymentEventHistory request);

        Task<PagedResponse<List<PaymentEventListResponse>>> GetListPaymentWithStatus(PagedRequest reqeust, int status);
    }
}
