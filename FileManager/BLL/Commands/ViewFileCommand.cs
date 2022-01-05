using System.Text;
using BLL.Abstractions.Interfaces;
using BLL.Utils;
using Core.Dataclasses;

namespace BLL.Commands
{
    internal class ViewFileCommand : Command
    {
        public override string Name => "vf";
        public const int symbolsNumber = 200;

        public override OptionalResult<string> Execute(string[] args)
        {
            string path = new ArgumentsValidator().ValidateNArguments(args, 1, Name)[0];
            using (var fs = File.OpenRead(path))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                char[] buffer = new char[symbolsNumber];
                int n = sr.ReadBlock(buffer, 0, symbolsNumber);

                var res = string.Join(string.Empty, buffer);

                return new OptionalResult<string>(!string.IsNullOrWhiteSpace(res) ? res : "<Empty File>");
            }
        }
    }
}
