using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.API;
using EventProduct.Application.Feature.EventProduct.Command.AddProduct;
using EventProduct.Application.Feature.EventProduct.Command.DeleteProduct;
using EventProduct.Application.Feature.EventProduct.Command.EditProduct;
using EventProduct.Application.Feature.EventProduct.Queries.GetAllProduct;
using EventProduct.Application.Feature.EventProduct.Queries.GetEventByCategory;
using EventProduct.Application.Feature.Statistical.StatisticalProductSaled;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventProduct.Presentation.V1
{

    [Route("api/v{version:apiVersion}/event-product")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventAPIProductController(ISender _sender) : BaseAPIController
    {
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody]AddProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Success(await _sender.Send(command),"Success");
        }

        [HttpPut("edit-product{productID}")]
        public async Task<IActionResult> EditProduct(Guid productID, [FromBody]EditProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            command.ProductID = productID;
            await _sender.Send(command);
            return Success("", "Update Success");
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct([FromQuery]DeleteProductCommand command)
        {
            await _sender.Send(command);
            return Success("", "Delete Success");
        }

        [HttpGet("get-all-product")]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductQuery query,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetAllProductQuery(query.Name)
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Success(await _sender.Send(request), "Success");
        }

        [HttpGet("get-product-by-category")]
        public async Task<IActionResult> GetProductByCategory([FromQuery] GetProductByCategoryQuery query, [FromQuery] int pageNumber = 1, [FromQuery,] int pageSize = 10)
        {
            var request = new GetProductByCategoryQuery(query.CategoryID,query.Name)
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Success(await _sender.Send(request),"Success");
        }

        [HttpGet("statistical-event-product-saled")]
        public async Task<IActionResult> StatisticalEventProductSaled([FromQuery]StatisticalProductSaledQuery query)
        {
            return Success(await _sender.Send(query),"Success");
        }
    }
}
