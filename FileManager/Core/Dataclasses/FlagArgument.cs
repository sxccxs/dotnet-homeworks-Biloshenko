namespace Core.DataClasses
{
    public class FlagArgument<T>
    {
        private readonly Func<FlagArgument<T>, T, T> applicationFunction;

        public FlagArgument(string flag, Func<FlagArgument<T>, T, T> apply, string[] subArguments = null)
        {
            this.Flag = flag;
            this.applicationFunction = apply;
            if (subArguments is null)
            {
                this.PossibleSubArguments = Array.Empty<string>();
            }
            else
            {
                this.PossibleSubArguments = subArguments;
            }
        }

        public string Flag { get; }

        public bool Exists { get; set; }

        public string[] PossibleSubArguments { get; }

        public string CurrentSubArgument { get; set; }

        public T Apply(T data)
        {
            return this.applicationFunction.Invoke(this, data);
        }
    }
}
