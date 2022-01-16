using BLL.Abstractions.Interfaces;
using BLL.Utilities;
using Core.DataClasses;

namespace BLL.Commands
{
    internal class MoveCommand : Command
    {
        public override string Name => "mv";

        public override OptionalResult<string> Execute(string[] arguments)
        {
            var path = new ArgumentsValidator().ValidateNArguments(arguments, 1, this.Name)[0];
            Directory.SetCurrentDirectory(path);

            return new OptionalResult<string>();
        }
    }
}
