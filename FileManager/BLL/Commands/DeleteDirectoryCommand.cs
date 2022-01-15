using BLL.Abstractions.Interfaces;
using BLL.Utilities;
using Core.DataClasses;

namespace BLL.Commands
{
    internal class DeleteDirectoryCommand : Command
    {
        public override string Name => "dd";

        public override OptionalResult<string> Execute(string[] arguments)
        {
            string path = new ArgumentsValidator().ValidateNArguments(arguments, 1, this.Name)[0];
            Directory.Delete(path, true);

            return new OptionalResult<string>();
        }
    }
}
