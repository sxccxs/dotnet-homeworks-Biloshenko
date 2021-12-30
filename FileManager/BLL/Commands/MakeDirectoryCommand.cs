using BLL.Abstractions.Interfaces;
using BLL.Utils;
using Core.Exceptions;

namespace BLL.Commands
{
    internal class MakeDirectoryCommand : Command
    {
        public override string Name => "md";

        public override string? Execute(string[] args)
        {
            var path = new ArgumentsValidator().ValidateNArguments(args, 1, Name)[0];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return null;
            }
            else
            {
                throw new CommandException($"Directory {path} already exists");
            }
        }
    }
}
