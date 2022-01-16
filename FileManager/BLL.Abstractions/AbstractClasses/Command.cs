using Core.DataClasses;

namespace BLL.Abstractions.Interfaces
{
    public abstract class Command
    {
        public Command()
        {
        }

        public abstract string Name { get; }

        public abstract OptionalResult<string> Execute(string[] arguments);
    }
}
