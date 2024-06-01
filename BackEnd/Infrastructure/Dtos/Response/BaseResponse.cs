namespace Infrastructure.Dtos.Response
{
    public class BaseResponse<R>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public R Result { get; set; }
        public ErrorMessage ErrorMessage { get; set; }

        public BaseResponse(bool success, string message, R result, ErrorMessage errorMessage, int statusCode)
        {
            Success = success;
            Message = message;
            Result = result;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public BaseResponse()
        {
        }
    }
}
