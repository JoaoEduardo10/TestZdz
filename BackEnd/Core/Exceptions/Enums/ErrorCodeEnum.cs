namespace Core.Exceptions.enums
{
    public enum ErrorCodeEnum : int
    {
        PRO0001,
        PRO0002,
        PRO0003,
        PRO0004,
        PRO0005,
        PRO0006,

        OR0001,
        OR0002,
        OR0003,
        OR0004,
        OR0005,
    }

    public static class ErrorCodeExtensions
    {
        private static readonly Dictionary<ErrorCodeEnum, (string Message, string Code)> ErrorCodeData = new Dictionary<ErrorCodeEnum, (string, string)>
        {
            { ErrorCodeEnum.PRO0001, ("Minimo de caracteres são 3", "PRO-0001")},
            { ErrorCodeEnum.PRO0002, ("Valor não pode ser menor ao igual a zero", "PRO-0002")},
            { ErrorCodeEnum.PRO0003, ("Não foi possivel cria o produto", "PRO-0003")},
            { ErrorCodeEnum.PRO0004, ("Produto não encontrado", "PRO-0004")},
            { ErrorCodeEnum.PRO0005, ("Não foi possivel atualizar o Produto", "PRO-0005")},
            { ErrorCodeEnum.PRO0006, ("Não foi possivel apagar o Produto", "PRO-0006")},


            { ErrorCodeEnum.OR0001, ("Não foi possivel criar o pedido", "OR-0001")},
            { ErrorCodeEnum.OR0002, ("Pedido não encontrado", "OR-0002")},
            { ErrorCodeEnum.OR0003, ("O valor total do pedido esta errado", "OR-0003")},
            { ErrorCodeEnum.OR0004, ("Não foi possivel atualizar o pedido", "OR-0004")},
            { ErrorCodeEnum.OR0005, ("Não foi possivel apagar o pedido", "OR-0005")},
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