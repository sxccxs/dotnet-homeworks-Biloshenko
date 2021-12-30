using Core.Dataclasses;

namespace BLL.Utils
{
    internal class OutputTextFormater
    {
        public string FormatSystemEntriesToTree(SystemEntryData[] data, string message)
        {
            var outputText = $"{message}:\n";
            foreach (var d in data)
            {
                var prefix = "    -";
                outputText += $"-{d.Name}{(d.ShowFullInfo ? ":" : "")}\n";

                if (!d.ShowFullInfo) continue;

                foreach (var (k, v) in d.Fields)
                {
                    outputText += $"{prefix}{k}: {v}\n";
                }
            }
            return outputText;
        }
    }

}
