namespace Core.Exceptions.enums
{
    public enum ErrorCodeEnum : int
    {
        PRO001,
        PRO002,
    }

    public static class ErrorCodeExtensions
    {
        private static readonly Dictionary<ErrorCodeEnum, (string Message, string Code)> ErrorCodeData = new Dictionary<ErrorCodeEnum, (string, string)>
        {
            { ErrorCodeEnum.PRO001, ("Minimo de caracteres são 3", "PRO-0001")},
            { ErrorCodeEnum.PRO002, ("valor não pode ser menor ao igual a zero", "PRO-0002")},

        };

        public static string GetMessage(this ErrorCodeEnum errorCode)
        {
            return ErrorCodeData[errorCode].Message;
        }

        public static string GetCode(this ErrorCodeEnum errorCode)
        {
            return ErrorCodeData[errorCode].Code;
        }

        public static string GetFormattedMessage(this ErrorCodeEnum errorCode, params object[] args)
        {
            string message = ErrorCodeData[errorCode].Message;
            return string.Format(message, args);
        }
    }
}