using BLL.Abstractions.Interfaces;
using BLL.Utils;
using Core.Dataclasses;
using Core.Exceptions;

namespace BLL.Commands
{
    internal class MakeFileCommand : Command
    {
        public override string Name => "mf";

        public override OptionalResult<string> Execute(string[] args)
        {
            var path = new ArgumentsValidator().ValidateNArguments(args, 1, Name)[0];
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
