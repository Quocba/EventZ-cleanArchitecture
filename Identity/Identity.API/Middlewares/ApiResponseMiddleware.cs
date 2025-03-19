using Identity.Application.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Base.Common;

namespace Identity.API.Middlewares
{
    public class ApiResponseMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);
                context.Response.Body = originalBodyStream;

                responseBody.Seek(0, SeekOrigin.Begin);
                var bodyContent = await new StreamReader(responseBody).ReadToEndAsync();
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(bodyContent);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, originalBodyStream, HttpStatusCode.NotFound, ex.Message);
            }
            catch (InvalidCredentialsException ex)
            {
                await HandleExceptionAsync(context, originalBodyStream, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, originalBodyStream, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, originalBodyStream, HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Stream originalBodyStream, HttpStatusCode statusCode, string message)
        {
            context.Response.Body = originalBodyStream;

            var apiResponse = new BaseResponse
            {
                Success = false,
                StatusCode = statusCode,
                Message = MessageByStatusCode.GetMessageByStatusCode((int)statusCode),
                Errors = new List<KeyValuePair<string, string>> { new("Error", message) }
            };

            var responseJson = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(responseJson);
        }
    }
}
