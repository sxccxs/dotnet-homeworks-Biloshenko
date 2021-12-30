using BLL.Abstractions.Interfaces;
using BLL.Utils;

namespace BLL.Commands
{
    internal class SearchInFileCommand : Command
    {
        public override string Name => "sif";

        public override string? Execute(string[] args)
        {
            args = new ArgumentsValidator().ValidateNArguments(args, 2, Name);
            var path = args[0];
            var query = args[1];
            var lines = File.ReadAllLines(path);
            string outputText = "";
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Contains(query))
                {
                    outputText += $"Found \"{query}\" in file {path} at line {i + 1}:\n";
                    outputText += $"    {line}\n";
                }
            }
            if (string.IsNullOrEmpty(outputText))
            {
                outputText = $"\"{query}\" was not found in {path}";
            }
            return outputText;
        }
    }
}
