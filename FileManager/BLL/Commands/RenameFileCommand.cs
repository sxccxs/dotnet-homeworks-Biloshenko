using BLL.Abstractions.Interfaces;
using BLL.Utilities;
using Core.DataClasses;

namespace BLL.Commands
{
    internal class RenameFileCommand : Command
    {
        public override string Name => "rf";

        public override OptionalResult<string> Execute(string[] arguments)
        {
            arguments = new ArgumentsValidator().ValidateNArguments(arguments, 2, this.Name);
            var old_path = arguments[0];
            var new_path = arguments[1];
            File.Move(old_path, new_path);

            return new OptionalResult<string>();
        }
    }
}
