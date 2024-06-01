namespace Core.Exceptions
{
    public class ClienteException : ApplicationException
    {
        public string Code { get; private set; }

        public ClienteException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}