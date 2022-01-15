using BLL.Abstractions.Interfaces;
using BLL.Commands;
using Core.Collections;
using Core.DataClasses;
using Core.Exceptions;

namespace BLL.Services
{
    internal class CommandParserService : IParserService
    {
        private readonly Command[] commands =
        {
            new DeleteDirectoryCommand(), new DeleteFileCommand(),
            new MakeDirectoryCommand(), new MakeFileCommand(),
            new RenameDirectoryCommand(), new RenameFileCommand(),
            new ViewFileCommand(), new SearchInFileCommand(),
            new MoveCommand(), new ShowCommand(),
        };

        private readonly CustomDictionary<string, Command> commandsDict;

        public CommandParserService()
        {
            this.commandsDict = new CustomDictionary<string, Command>();
            foreach (var command in this.commands)
            {
                this.commandsDict.Add(command.Name, command);
            }
        }

        public OptionalResult<string> Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new OptionalResult<string>();
            }

            var splittedInput = this.SplitString(input, ' ');
            var command = splittedInput.First();
            var arguments = splittedInput.Skip(1).ToArray();
            if (this.commandsDict.Contains(command))
            {
                return this.commandsDict[command].Execute(arguments);
            }
            else
            {
                throw new InvalidCommandException($"Command {command} not found");
            }
        }

        private string[] SplitString(string stringToSplit, char sep)
        {
            var skip = false;
            var splitted = new CustomLinkedList<string>();
            for (int i = 0; i < stringToSplit.Length; i++)
            {
                if (stringToSplit[i] == '"')
                {
                    skip = !skip;
                }
                else if (!skip && stringToSplit[i] == sep)
                {
                    splitted.Add(stringToSplit[0..i]);
                    stringToSplit = stringToSplit[(i + 1) ..];
                    i = -1;
                }
            }

            splitted.Add(stringToSplit);
            for (var i = 0; i < splitted.Count; i++)
            {
                splitted[i] = string.Join(string.Empty, splitted[i].ToCharArray()
                                                       .Where(x => x != '"')
                                                       .ToArray());
            }

            return splitted.ToArray();
        }
    }
}
