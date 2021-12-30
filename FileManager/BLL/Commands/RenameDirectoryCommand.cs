using BLL.Abstractions.Interfaces;
using BLL.Utils;

namespace BLL.Commands
{
    internal class RenameDirectoryCommand : Command
    {
        public override string Name => "rd";

        public override string? Execute(string[] args)
        {
            args = new ArgumentsValidator().ValidateNArguments(args, 2, Name);
            var old_path = args[0];
            var new_path = args[1];
            Directory.Move(old_path, new_path);
            return null;
        }
    }
}
