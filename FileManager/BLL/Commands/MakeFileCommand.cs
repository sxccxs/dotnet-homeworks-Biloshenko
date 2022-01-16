using BLL.Abstractions.Interfaces;
using BLL.Utilities;
using Core.DataClasses;
using Core.Exceptions;

namespace BLL.Commands
{
    internal class MakeFileCommand : Command
    {
        public override string Name => "mf";

        public override OptionalResult<string> Execute(string[] arguments)
        {
            var path = new ArgumentsValidator().ValidateNArguments(arguments, 1, this.Name)[0];
            if (!File.Exists(path))
            {
                File.Create(path).Close();

                return new OptionalResult<string>();
            }
            else
            {
                throw new CommandException($"File at {path} already exists");
            }
        }
    }
}
