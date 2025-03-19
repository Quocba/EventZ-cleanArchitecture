using Asp.Versioning;
using Base.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.Application.Features.PaymentEvent.Commands.CreatePaymentEvent;
using Payment.Application.Features.PaymentEvent.Commands.PaymentEventReturn;
using Payment.Application.Features.PaymentEvent.Commands.PaymentManual;
using Payment.Application.Features.PaymentEvent.Queries.GetListPaymentEvent;
using Payment.Application.Features.PaymentEvent.Queries.GetListPaymentEventHistory;
using Payment.Application.Features.PaymentEvent.Queries.GetPaymentByStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Presentation.V1
{
    [Route("api/v{version:apiVersion}/payment")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PaymentEventControllerV1(ISender _sender) : BaseAPIController
    {
        [HttpGet("payment-events")]
        public async Task<IActionResult> GetListPaymentEvent([FromQuery] GetListPaymentEventQuery query)
        {
            var paymentEvents = await _sender.Send(query);
            return Success(paymentEvents, "Get list payment event successful");
        }

        [HttpGet("payment-events-history")]
        public async Task<IActionResult> GetListPaymentEventHistory([FromHeader(Name = "X-User-Id")] Guid userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var paymentEvents = await _sender.Send(new GetListPaymentEventHistoryQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = userId
            });
            return Success(paymentEvents, "Get list payment event history successful");
        }

        [HttpPost("create-payment-event")]
        public async Task<IActionResult> CreatePaymentEvent([FromHeader(Name = "X-User-Id")] Guid userId, [FromBody] CreatePaymentEventCommand command)
        {
            command.UserId = userId;
            var linkPayment = await _sender.Send(command);
            return Success(linkPayment, "Create successful event payment");
        }

        [HttpGet("payment-event-return")]
        public async Task<IActionResult> PaymentEventReturn([FromQuery] PaymentEventReturnCommand command)
        {
            var linkPayment = await _sender.Send(command);
            return Redirect(linkPayment);
        }

        [HttpPost("payment-event-manual")]
        public async Task<IActionResult> PaymentEventManual([FromHeader(Name = "X-User-Id")] Guid userId, [FromBody] PaymentEventManualCommand command)
        {
            command.UserId = userId;
            await _sender.Send(command);
            return Success(null, "Create successful event payment manual");
        }

        [HttpGet("get-payment-by-status")]
        public async Task<IActionResult> GetPaymentByStatus([FromQuery]int status, [FromQuery]int pageNubmer = 1, [FromQuery]int pageSize = 10)
        {
            var request = new GetPaymentByStatusQuery(status) {PageNumber = pageNubmer, PageSize = pageSize};
            return Success(await _sender.Send(request), "Success");
    }
}
    }
