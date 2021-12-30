using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dataclasses
{
    public class FlagArgument<T>
    {
        public string Flag { get; }

        public bool Exists { get; set; }

        public string[] PossibleSubArguments { get; }

        public string? CurrentSubArgument { get; set; }

        private readonly Func<FlagArgument<T>, T, T> _applyFunc;

        public FlagArgument(string flag, Func<FlagArgument<T>, T, T> apply, string[]? subArguments = null)
        {
            Flag = flag;
            _applyFunc = apply;
            if (subArguments is null)
            {
                PossibleSubArguments = Array.Empty<string>();
            }
            else
            {
                PossibleSubArguments = subArguments;
            }

        }
        public T Apply(T data)
        {
            return _applyFunc.Invoke(this, data);
        }
    }
}
