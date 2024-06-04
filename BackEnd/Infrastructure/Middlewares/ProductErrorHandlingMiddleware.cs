using Core.Exceptions.enums;
using Core.Exceptions;
using Infrastructure.Dtos.Response;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares
{
    public class ProductErrorHandlingMiddleware
    {
        private readonly RequestDelegate _Next;

        public ProductErrorHandlingMiddleware(RequestDelegate next)
        {
            _Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next(context);
            }
            catch (ProductException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ProductException error)
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
