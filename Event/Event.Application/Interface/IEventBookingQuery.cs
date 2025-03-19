using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MassTransit;

namespace Event.Application.Interface
{
    public interface IEventBookingQuery
    {
        Task<PagedResponseWithTotal<List<EventBookingDTO>>> GetTotalNumberAddtendess(PagedRequest request, Guid eventID);
        Task<PagedResponseWithTotal<List<EventBookingDTO>>> GetBookingWithStatus(PagedRequest request, Guid eventID, int status);
    }
}
