using System.Text;
using BLL.Abstractions.Interfaces;
using BLL.Utilities;
using Core.DataClasses;

namespace BLL.Commands
{
    internal class ViewFileCommand : Command
    {
        public const int SymbolsNumber = 200;

        public override string Name => "vf";

        public override OptionalResult<string> Execute(string[] arguments)
        {
            string path = new ArgumentsValidator().ValidateNArguments(arguments, 1, this.Name)[0];
            using (var fileStream = File.OpenRead(path))
            using (var sr = new StreamReader(fileStream, Encoding.UTF8))
            {
                char[] buffer = new char[SymbolsNumber];
                int n = sr.ReadBlock(buffer, 0, SymbolsNumber);

                var res = string.Join(string.Empty, buffer);

                return new OptionalResult<string>(!string.IsNullOrWhiteSpace(res) ? res : "<Empty File>");
            }
        }
    }
}
