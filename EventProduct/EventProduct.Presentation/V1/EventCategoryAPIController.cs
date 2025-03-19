using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using EventProduct.Application.Feature.EventCategory.Command.AddCategory;
using EventProduct.Application.Feature.EventCategory.Command.DeleteCategory;
using EventProduct.Application.Feature.EventCategory.Command.EditCategory;
using EventProduct.Application.Feature.EventCategory.Queries.GetAllCategory;
using EventProduct.Application.Feature.EventCategory.Queries.GetEventByCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventProduct.Presentation.V1
{

    [Route("api/v{version:apiVersion}/event-category")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventCategoryAPIController(ISender _sender) : BaseAPIController
    {
        [HttpPost("add-event-category")]
        public async Task<IActionResult> AddEventCategory([FromBody] AddCategoryCommand command)
        {
            return Success(await _sender.Send(command), "Success");
        }

        [HttpPut("edit-event-category/{categoryID}")]
        public async Task<IActionResult> EditEventCategory(Guid categoryID, [FromBody] EditCategoryCommand command)
        {
            await _sender.Send(new { CategoryID = categoryID, Command = command });
            return Success("", "Success");
        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory([FromQuery] DeleteCategoryCommand command)
        {
            await _sender.Send(command);
            return Success(null, "Delete Success");
        }

        [HttpGet("get-all-category")]
        public async Task<IActionResult> GetAllCategory([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetAllCategoryQuery { PageNumber = pageNumber, PageSize = pageSize };
            return Success(await _sender.Send(request), "Success");
        }

        [HttpGet("get-category-by-event")]
        public async Task<IActionResult> GetEventByCategory([FromQuery] Guid EventID, [FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var request = new GetCategoryByEventQuery(EventID)
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Success(await _sender.Send(request), "Success");
        }
    }
}
