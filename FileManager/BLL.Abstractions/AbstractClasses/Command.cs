using Core.Dataclasses;

namespace BLL.Abstractions.Interfaces
{

    public abstract class Command
    {
        public Command() { }
        public abstract string Name { get; }
        public abstract OptionalResult<string> Execute(string[] args);
    }
}
