using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;

namespace EventProduct.Domain.Entities
{
    public interface IEventOrderQuery
    {
        Task<PagedResponse<List<EventOrderDTO>>> GetEventOrderByPayment(PagedRequest request, Guid paymentID);
        Task<PagedResponse<List<EventOrderDTO>>> GetEventOrderByUser(PagedRequest request, Guid userID);
        Task<PagedResponse<List<EventOrderDTO>>> GetEventOrderByEvent(PagedRequest request, Guid eventID);
        Task<int> SumaryEventOrder(Guid eventID);
        Task<Decimal> TotalRevenueProduct(Guid eventID);

    }
}
