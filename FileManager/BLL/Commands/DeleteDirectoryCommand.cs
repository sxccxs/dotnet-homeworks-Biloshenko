using BLL.Abstractions.Interfaces;
using BLL.Utils;
using Core.Dataclasses;

namespace BLL.Commands
{
    internal class DeleteDirectoryCommand : Command
    {
        public override string Name => "dd";

        public override OptionalResult<string> Execute(string[] args)
        {
            string path = new ArgumentsValidator().ValidateNArguments(args, 1, Name)[0];
            Directory.Delete(path, true);

            return new OptionalResult<string>();
        }
    }
}
