using Core.Exceptions;
using Core.Exceptions.enums;
using Infrastructure.Dtos.Response;
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
            }catch (OrderException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, OrderException error)
        {
            string defaultMessage = "Falha na requisição";

            if (error.Code == ErrorCodeEnum.OR0001.GetCode())
            {
                int statusCode = 400;

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

                return context.Response.WriteAsync(response);

            }

            if (error.Code == ErrorCodeEnum.OR0002.GetCode())
            {
                int statusCode = 404;

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

                return context.Response.WriteAsync(response);

            }

            if (error.Code == ErrorCodeEnum.OR0003.GetCode())
            {
                int statusCode = 400;

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

                return context.Response.WriteAsync(response);

            }

            int statusCodeInternal = 500;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCodeInternal;

            var responseInternal = JsonSerializer.Serialize(
                new BaseResponse<object>
                {
                    Message = defaultMessage,
                    StatusCode = statusCodeInternal,
                    Success = false,
                    ErrorMessage = new ErrorMessage(error.Message, "null"),
                    Result = new { }
                }
            );

            return context.Response.WriteAsync(responseInternal);
        }
    }
}
