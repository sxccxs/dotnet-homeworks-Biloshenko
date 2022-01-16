using BLL.Abstractions.Interfaces;
using BLL.Utilities;
using Core.DataClasses;
using Core.Exceptions;

namespace BLL.Commands
{
    internal class MakeDirectoryCommand : Command
    {
        public override string Name => "md";

        public override OptionalResult<string> Execute(string[] arguments)
        {
            var path = new ArgumentsValidator().ValidateNArguments(arguments, 1, this.Name)[0];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                return new OptionalResult<string>();
            }
            else
            {
                throw new CommandException($"Directory {path} already exists");
            }
        }
    }
}
