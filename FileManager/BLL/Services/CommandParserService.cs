using BLL.Abstractions.Interfaces;
using BLL.Commands;
using Core.Collections;
using Core.Dataclasses;
using Core.Exceptions;

namespace BLL.Services
{
    internal class CommandParserService : IParserService
    {
        private readonly Command[] _commands =  { new DeleteDirectoryCommand(), new DeleteFileCommand(),
                                                   new MakeDirectoryCommand(), new MakeFileCommand(),
                                                   new RenameDirectoryCommand(), new RenameFileCommand(),
                                                   new ViewFileCommand(), new SearchInFileCommand(),
                                                   new MoveCommand(), new ShowCommand(),
                                                 };
        private readonly CustomDictionary<string, Command> _commandsDict;

        public CommandParserService()
        {
            _commandsDict = new CustomDictionary<string, Command>();
            foreach (var command in _commands)
            {
                _commandsDict.Add(command.Name, command);
            }
        }
        private string[] SplitString(string s, char sep)
        {
            var skip = false;
            var splited = new CustomLinkedList<string>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '"')
                {
                    skip = !skip;
                }
                else if (!skip && s[i] == sep)
                {
                    splited.Add(s[0..i]);
                    s = s[(i + 1)..];
                    i = -1;
                }

            }
            splited.Add(s);
            for (var i = 0; i < splited.Count; i++)
            {
                splited[i] = string.Join(string.Empty, splited[i].ToCharArray()
                                                       .Where(x => x != '"')
                                                       .ToArray());
            }

            return splited.ToArray();
        }

        public OptionalResult<string> Parse(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new OptionalResult<string>();
            }
            var inp = SplitString(input, ' ');
            var cmd = inp.First();
            var mArgs = inp.Skip(1).ToArray();
            if (_commandsDict.Contains(cmd))
            {
                return _commandsDict[cmd].Execute(mArgs);
            }
            else
            {
                throw new InvalidCommandException($"Command {cmd} not found");
            }
        }
    }
}
