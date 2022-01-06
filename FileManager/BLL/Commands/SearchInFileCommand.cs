using BLL.Abstractions.Interfaces;
using BLL.Utils;
using Core.Dataclasses;

namespace BLL.Commands
{
    internal class SearchInFileCommand : Command
    {
        public override string Name => "sif";

        public override OptionalResult<string> Execute(string[] args)
        {
            args = new ArgumentsValidator().ValidateNArguments(args, 2, Name);
            var path = args[0];
            var query = args[1];
            var lines = File.ReadAllLines(path);
            var outputText = FindQueryInStringArray(lines, query, path);
            if (string.IsNullOrEmpty(outputText))
            {
                outputText = $"\"{query}\" was not found in {path}";
            }

            return new OptionalResult<string>(outputText);
        }

        private string FindQueryInStringArray(string[] textLines, string query, string path)
        {
            var outputText = string.Empty;
            for (var i = 0; i < textLines.Length; i++)
            {
                var line = textLines[i];
                if (line.Contains(query))
                {
                    outputText += $"Found \"{query}\" in file {path} at line {i + 1}:\n";
                    outputText += $"    {line}\n";
                }
            }
            return outputText;
        }
    }
}
