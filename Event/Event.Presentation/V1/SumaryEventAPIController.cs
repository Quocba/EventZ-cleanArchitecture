using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using Event.Application.Feature.SumaryEvent.Queries.EventWaitForApproval;
using Event.Application.Feature.SumaryEvent.Queries.GetBookingByStatus;
using Event.Application.Feature.SumaryEvent.Queries.SumaryRegistrationEventToDay;
using Event.Application.Feature.SumaryEvent.Queries.TotalNumberOfAttendees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/sumary-event")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SumaryEventAPIController(ISender _sender) : BaseAPIController
    {
        [HttpGet("sumary-event-by-status")]
        public async Task<IActionResult> SumaryEventByStatus([FromQuery] SumaryEventWithStatusQuery query)
        {
            return Success(await _sender.Send(query), "Success");
        }

        [HttpGet("get-registration-event-today")]
        public async Task<IActionResult> SumaryRegistrationEventToDay([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var reqeust = new SumaryRegistrationEventToDayQuery { PageNumber = pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(reqeust), "Success");
        }

        [HttpGet("total-number-of-attendees")]
        public async Task<IActionResult> TotalNumberAttendess([FromQuery] Guid EventID, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new TotalNumberOfAttendeesQuery(EventID) { pageNUmber = pageNumber, pageSize = pageSize };
            var response = await _sender.Send(request);
            return Success(response, "Success");
        }

        [HttpGet("get-booking-by-status")]
        public async Task<IActionResult> GetBookingWithStatus([FromQuery]int status, [FromQuery]Guid eventID, int pageNumber = 1, int pageSize = 10)
        {
            var reqeust = new GetBookingByStatusQuery(eventID,status){ PageNumber = pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(reqeust), "Success");
        }
    }
}
