namespace Core.Exceptions
{
    public class PedidosException : ApplicationException
    {
        public string Code { get; private set; }

        public PedidosException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}
