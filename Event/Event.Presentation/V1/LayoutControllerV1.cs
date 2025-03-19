using Base.API;
using Event.Application.Feature.Layout.Commands.CreateLayout;
using Event.Application.Feature.Layout.Commands.DeleteLayout;
using Event.Application.Feature.Layout.Commands.UpdateLayout;
using Event.Application.Feature.Layout.Queries.GetAllLayout;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Presentation.V1
{
    [Route("api/v{version:apiVersion}/layout")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LayoutControllerV1(ISender _sender) : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllLayoutQuery request)
        {
            var layouts = await _sender.Send(request);
            return Success(layouts, "Get all layout successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "X-User-Id")] Guid userId, [FromBody] CreateLayoutCommand command)
        {
            command.CreatedBy = userId.ToString();
            var id = await _sender.Send(command);
            return Success(id, "Create layout successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] UpdateLayoutCommand command)
        {
            command.Id = id;
            await _sender.Send(command);
            return Success(id, "Update layout successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _sender.Send(new DeleteLayoutCommand { LayoutId = id });
            return Success(id, "Delete layout successfully.");
        }
    }
}
