namespace Core.Exceptions
{
    public class DublicateKeyException : Exception
    {
        public DublicateKeyException() { }
        public DublicateKeyException(string message) : base(message) { }
    }
}
