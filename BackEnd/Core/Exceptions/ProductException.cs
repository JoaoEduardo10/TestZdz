namespace Core.Exceptions
{
    public class ProductException : ApplicationException
    {
        public string Code { get; private set; }

        public ProductException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}