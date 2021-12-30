using BLL.Abstractions.Interfaces;
using BLL.Utils;

namespace BLL.Commands
{
    internal class RenameFileCommand : Command
    {
        public override string Name => "rf";

        public override string? Execute(string[] args)
        {
            args = new ArgumentsValidator().ValidateNArguments(args, 2, Name);
            var old_path = args[0];
            var new_path = args[1];
            File.Move(old_path, new_path);
            return null;
        }
    }
}
