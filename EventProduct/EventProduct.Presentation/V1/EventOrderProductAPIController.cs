using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using EventProduct.Application.Feature.EventOrder.Command.EditEventOrder;
using EventProduct.Application.Feature.EventProductOrder.Command.CreateEventOrderProduct;
using EventProduct.Application.Feature.EventProductOrder.Command.DeleteEventOrderProduct;
using EventProduct.Application.Feature.EventProductOrder.Command.EditQuantityEventOrderProduct;
using EventProduct.Application.Feature.EventProductOrder.Queries.GetEventProductOrderByEventOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventProduct.Presentation.V1
{
    [Route("api/v{version:apiVersion}/event-order-product")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventOrderProductAPIController(ISender _sender) : BaseAPIController
    {
        [HttpPost("create-event-product-order")]
        public async Task<IActionResult> CreateEventProductOder([FromBody]CreateEventProductOrderCommand command)
        {
            return Success(await _sender.Send(command), "Success");
        }

        [HttpDelete("delete-event-order-product")]
        public async Task<IActionResult> DeleteEventOrderProduct([FromQuery] DeleteEventOrderProductCommand command)
        {
            await _sender.Send(command);
            return Success("", "Success");
        }

        [HttpPut("edit-quantity-event-order-product{eventProductOrderID}")]
        public async Task<IActionResult> EditQuantityEventOrderProduct(Guid eventProductOrderID,[FromBody]EditEventProductOrderCommand command)
        {
            command.EventProductOrderID = eventProductOrderID;
            await _sender.Send(command);
            return Success("", "Update Success");
        }

        [HttpGet("get-event-product-order-by-event-order")]
        public async Task<IActionResult> GetEventProductOrderByEventOrder([FromQuery]GetEventProductOrderByEventOrderQuery query,
                                                                          [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetEventProductOrderByEventOrderQuery(query.EventOrderID)
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Success(await _sender.Send(request), "Success");
        }



    }
}
