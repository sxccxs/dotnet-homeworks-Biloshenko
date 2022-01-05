using BLL.Abstractions.Interfaces;
using BLL.Utils;
using Core.Dataclasses;

namespace BLL.Commands
{
    internal class DeleteFileCommand : Command
    {
        public override string Name => "df";

        public override OptionalResult<string> Execute(string[] args)
        {
            string path = new ArgumentsValidator().ValidateNArguments(args, 1, Name)[0];
            File.Delete(path);

            return new OptionalResult<string>();
        }
    }
}
