namespace Infrastructure.Dtos.Response
{
    public class ErrorMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ErrorMessage( string message, string code)
        {
            Code = code;
            Message = message;
        }

        public ErrorMessage()
        {
        }
    }
}
