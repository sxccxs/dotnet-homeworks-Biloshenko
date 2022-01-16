using BLL.Abstractions.Interfaces;
using BLL.Utilities;
using Core.DataClasses;
using Core.Enumerates;

namespace BLL.Commands
{
    internal class ShowCommand : Command
    {
        private readonly FlagArgument<SystemEntryData[]>[] flagArguments;
        private readonly string[] sortingCriteria = { "size", "type", "created", "name" };

        public ShowCommand()
        {
            this.flagArguments = new FlagArgument<SystemEntryData[]>[]
            {
                new FlagArgument<SystemEntryData[]>("-h", this.ApplyHidden),
                new FlagArgument<SystemEntryData[]>("-f", this.ApplyShowFullInfo),
                new FlagArgument<SystemEntryData[]>("-s", this.ApplySort, this.sortingCriteria),
            };
        }

        public override string Name => "sh";

        public override OptionalResult<string> Execute(string[] arguments)
        {
            new ArgumentsValidator().ValidateParameters(arguments, this.flagArguments, this.Name);
            var systemFormatter = new SystemEntriesFormatter();

            var data = systemFormatter.GetSystemEntryDataForAllEntries();

            foreach (var argument in this.flagArguments)
            {
                data = argument.Apply(data);
            }

            string outputText = string.Empty;
            var textFormatter = new OutputTextFormatter();
            var directoriesData = systemFormatter.FilterSystemEntryDataByFlag(data, SystemEntryType.Directory);
            var filesData = systemFormatter.FilterSystemEntryDataByFlag(data, SystemEntryType.File);

            outputText += textFormatter.FormatSystemEntriesToTree(directoriesData, "Directories");
            outputText += "\n";
            outputText += textFormatter.FormatSystemEntriesToTree(filesData, "Files");

            return new OptionalResult<string>(outputText);
        }

        private SystemEntryData[] ApplyHidden(FlagArgument<SystemEntryData[]> flag, SystemEntryData[] data)
        {
            if (!flag.Exists)
            {
                data = data.Where(x => !x.IsHidden).ToArray();
            }

            return data;
        }

        private SystemEntryData[] ApplyShowFullInfo(FlagArgument<SystemEntryData[]> flag, SystemEntryData[] data)
        {
            if (flag.Exists)
            {
                Array.ForEach(data, x => x.ShowFullInfo = true);
            }

            return data;
        }

        private SystemEntryData[] ApplySort(FlagArgument<SystemEntryData[]> flag, SystemEntryData[] data)
        {
            if (flag.Exists && flag.CurrentSubArgument is not null)
            {
                data = this.SortDirectories(data, flag.CurrentSubArgument)
                                           .Concat(this.SortFiles(data, flag.CurrentSubArgument))
                                           .ToArray();
            }

            return data;
        }

        private SystemEntryData[] SortFiles(SystemEntryData[] data, string key)
        {
            var files = data.Where(x => x.Type == SystemEntryType.File).ToArray();
            if (files.Where(x => x.Fields.Contains(key)).ToArray().Length != files.Length)
            {
                return files.OrderBy(x => x.Name).ToArray();
            }

            return files.OrderBy(x => x.Fields[key]).ToArray();
        }

        private SystemEntryData[] SortDirectories(SystemEntryData[] data, string key)
        {
            var files = data.Where(x => x.Type == SystemEntryType.Directory).ToArray();
            if (files.Where(x => x.Fields.Contains(key)).ToArray().Length != files.Length)
            {
                return files.OrderBy(x => x.Name).ToArray();
            }

            return files.OrderBy(x => x.Fields[key]).ToArray();
        }
    }
}
