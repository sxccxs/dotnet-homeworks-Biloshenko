using BLL.Abstractions.Interfaces;
using BLL.Utils;

namespace BLL.Commands
{
    internal class DeleteFileCommand : Command
    {
        public override string Name => "df";

        public override string? Execute(string[] args)
        {
            string path = new ArgumentsValidator().ValidateNArguments(args, 1, Name)[0];
            File.Delete(path);
            return null;
        }
    }
}
