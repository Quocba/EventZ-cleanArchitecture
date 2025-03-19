using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using EventProduct.Application.Feature.EventOrder.Command.CreateEventOrder;
using EventProduct.Application.Feature.EventOrder.Command.EditEventOrder;
using EventProduct.Application.Feature.EventOrder.Queries.GetEventOrderByEvent;
using EventProduct.Application.Feature.EventOrder.Queries.GetEventOrderByPayment;
using EventProduct.Application.Feature.Statistical.EventOrder;
using EventProduct.Application.Feature.Statistical.Total_Revenue;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventProduct.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event-order")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventOrderAPIController(ISender _sender) : BaseAPIController
    {
        [HttpPost("create_event_order")]
        public async Task<IActionResult> CreateEventOrder([FromBody] CreateEventOrderCommand command)
        {
            return Success(await _sender.Send(command), "Create Order Success");
        }

        [HttpGet("get-event-order-by-payment")]
        public async Task<IActionResult> GetEventOrderByPayment([FromQuery]GetEventOrderByPaymentQuery query, [FromQuery]int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetEventOrderByPaymentQuery(query.PaymentHistoryID) { PageNumber = pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(query), "Success");
        }

        [HttpGet("get-event-order-by-user")]
        public async Task<IActionResult> GetEventOrderByUser([FromQuery] GetEventOrderByUserQuery query,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetEventOrderByUserQuery(query.UserID) { PageNumber = pageNumber, PageSize = pageSize };

            return Success(await _sender.Send(request), "Success");
        }

        [HttpGet("get-event-order-by-event")]
        public async Task<IActionResult> GetEventOrderByEvent([FromQuery] GetEventOrderByEventQuery query, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetEventOrderByEventQuery(query.EventID) { PageNumber = pageNumber, PageSize = pageSize };

            return Success(await _sender.Send(request), "Success");
        }

        [HttpPut("edit-event-order/{eventOrderID}")]
        public async Task<IActionResult> EditEventOrder(Guid eventOrderID, EditEventOrderCommand command)
        {
            command.EventOrderID = eventOrderID;
            await _sender.Send(command);
            return Success("", "Update Success");
        }

        [HttpGet("sumary-event-order")]
        public async Task<IActionResult> SumaryEventOrder([FromQuery]SumaryEventOrderByEventQuery query)
        {
            return Success(await _sender.Send(query), "Success");
        }

        [HttpGet("total-revenue-product")]
        public async Task<IActionResult> TotalRevenueProduct([FromQuery] TotalRevenueProductQuery query)
        {
            return Success(await _sender.Send(query), "Success");
        }

    }
}
