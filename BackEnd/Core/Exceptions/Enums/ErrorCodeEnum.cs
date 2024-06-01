namespace Core.Exceptions.enums
{
    public enum ErrorCodeEnum : int
    {
        PRO0001,
        PRO0002,
        PRO0003,

        PEO0001,
        PEO0002,
    }

    public static class ErrorCodeExtensions
    {
        private static readonly Dictionary<ErrorCodeEnum, (string Message, string Code)> ErrorCodeData = new Dictionary<ErrorCodeEnum, (string, string)>
        {
            { ErrorCodeEnum.PRO0001, ("Minimo de caracteres são 3", "PRO-0001")},
            { ErrorCodeEnum.PRO0002, ("Valor não pode ser menor ao igual a zero", "PRO-0002")},
            { ErrorCodeEnum.PRO0003, ("Não foi possivel cria o produto", "PRO-0003")},

            { ErrorCodeEnum.PEO0001, ("Não foi possivel criar o pedido", "PE-0001")},
            { ErrorCodeEnum.PEO0002, ("Pedido não encontrado", "PE-0002")},
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