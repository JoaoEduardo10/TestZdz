using Core.Exceptions;
using Core.Exceptions.enums;
using Infrastructure.Dtos.Response;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Middlewares
{
    public class OrderErrorHandlingMiddleware
    {
        private readonly RequestDelegate _Next;

        public OrderErrorHandlingMiddleware(RequestDelegate next)
        {
            _Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next(context);
            }
            catch (OrderException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, OrderException error)
        {
            string defaultMessage = "Falha na requisição";
            int statusCode = GetStatusCode(error.Code);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = JsonSerializer.Serialize(
                new BaseResponse<object>
                {
                    Message = defaultMessage,
                    StatusCode = statusCode,
                    Success = false,
                    ErrorMessage = new ErrorMessage(error.Message, error.Code),
                    Result = new { }
                }
            );

            await context.Response.WriteAsync(response);
        }

        private int GetStatusCode(string errorCode)
        {
            return errorCode switch
            {
                _ when errorCode == ErrorCodeEnum.PRO0001.GetCode() => 400,
                _ when errorCode == ErrorCodeEnum.PRO0002.GetCode() => 400,
                _ when errorCode == ErrorCodeEnum.PRO0003.GetCode() => 404,
                _ when errorCode == ErrorCodeEnum.PRO0004.GetCode() => 404,
                _ when errorCode == ErrorCodeEnum.PRO0005.GetCode() => 409,
                _ => 400
            };
        }
    }
}
