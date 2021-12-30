using BLL.Abstractions.Interfaces;
using BLL.Utils;

namespace BLL.Commands
{
    internal class MoveCommand : Command
    {
        public override string Name => "mv";
        public override string? Execute(string[] args)
        {
            var path = new ArgumentsValidator().ValidateNArguments(args, 1, Name)[0];
            Directory.SetCurrentDirectory(path);
            return null;

        }
    }
}
