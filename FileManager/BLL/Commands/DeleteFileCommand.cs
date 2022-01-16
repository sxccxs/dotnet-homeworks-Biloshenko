using BLL.Abstractions.Interfaces;
using BLL.Utilities;
using Core.DataClasses;

namespace BLL.Commands
{
    internal class DeleteFileCommand : Command
    {
        public override string Name => "df";

        public override OptionalResult<string> Execute(string[] arguments)
        {
            string path = new ArgumentsValidator().ValidateNArguments(arguments, 1, this.Name)[0];
            File.Delete(path);

            return new OptionalResult<string>();
        }
    }
}
