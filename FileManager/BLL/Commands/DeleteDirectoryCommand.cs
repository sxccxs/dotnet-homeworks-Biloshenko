using BLL.Abstractions.Interfaces;
using BLL.Utils;

namespace BLL.Commands
{
    internal class DeleteDirectoryCommand : Command
    {
        public override string Name => "dd";

        public override string? Execute(string[] args)
        {
            string path = new ArgumentsValidator().ValidateNArguments(args, 1, Name)[0];
            Directory.Delete(path, true);
            return null;
        }
    }
}
