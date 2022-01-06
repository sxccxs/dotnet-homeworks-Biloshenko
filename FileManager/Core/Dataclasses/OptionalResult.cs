namespace Core.Dataclasses
{
    public class OptionalResult<T>
    {
        private readonly T? _value;

        public bool HasValue { get { return _value is not null; } }

        public T Value
        {
            get
            {
                return _value is not null ? _value :
                                            throw new InvalidOperationException("Can't get value from empty result.");
            }
        }

        public OptionalResult() { }

        public OptionalResult(T value)
        {
            _value = value;
        }

    }
}
