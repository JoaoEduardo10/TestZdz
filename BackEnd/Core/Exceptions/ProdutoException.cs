namespace Core.Exceptions
{
    public class ProdutoException : ApplicationException
    {
        public string Code { get; private set; }

        public ProdutoException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}