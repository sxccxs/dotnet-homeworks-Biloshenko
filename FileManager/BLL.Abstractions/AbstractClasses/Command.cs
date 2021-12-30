namespace BLL.Abstractions.Interfaces
{

    public abstract class Command
    {
        public Command() { }
        public abstract string Name { get; }
        public abstract string? Execute(string[] args);
    }
}
