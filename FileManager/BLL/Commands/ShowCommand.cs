using BLL.Utils;
using Core.Dataclasses;
using BLL.Abstractions.Interfaces;
using Core.Enumerates;

namespace BLL.Commands
{
    internal class ShowCommand : Command
    {
        private readonly FlagArgument<SystemEntryData[]>[] _flagArguments;
        private readonly string[] _sortingCriterias = { "size", "type", "created", "name" };

        public ShowCommand()
        {
            _flagArguments = new FlagArgument<SystemEntryData[]>[]
            {
                new FlagArgument<SystemEntryData[]>("-h", ApplyHidden),
                new FlagArgument<SystemEntryData[]>("-f", ApplyShowFullInfo),
                new FlagArgument<SystemEntryData[]>("-s", ApplySort, _sortingCriterias),

            };
        }

        public override string Name => "sh";

        public override string? Execute(string[] args)
        {
            new ArgumentsValidator().ValidateParameters(args, _flagArguments, Name);
            var sysFormater = new SystemEntriesFormater();


            var data = sysFormater.GetSystemEntryDataForAllEntriess();

            foreach (var arg in _flagArguments)
            {
                data = arg.Apply(data);
            }

            string outputText = "";
            var textFormater = new OutputTextFormater();
            var dirsData = sysFormater.FilterSystemEntryDataByFlag(data, SystemEntryType.Directory);
            var filesData = sysFormater.FilterSystemEntryDataByFlag(data, SystemEntryType.File);

            outputText += textFormater.FormatSystemEntriesToTree(dirsData, "Directories");
            outputText += "\n";
            outputText += textFormater.FormatSystemEntriesToTree(filesData, "Files");
            return outputText;
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
                data = SortDirectories(data, flag.CurrentSubArgument)
                                       .Concat(SortFiles(data, flag.CurrentSubArgument))
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
