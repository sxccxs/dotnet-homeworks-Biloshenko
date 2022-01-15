using Core.DataClasses;
using Core.Exceptions;

namespace BLL.Utilities
{
    internal class ArgumentsValidator
    {
        public string[] ValidateNArguments(string[] arguments, int numberOfArguments, string commandName)
        {
            if (arguments is null || arguments?.Length < numberOfArguments)
            {
                throw new InvalidArgumentException($"{commandName} command needs {numberOfArguments} argument" +
                                                   $"{(numberOfArguments.ToString().Last() == '1' ? string.Empty : "s")}," +
                                                   $" {arguments?.Length} given.");
            }
            else if (arguments?.Length > numberOfArguments)
            {
                throw new InvalidArgumentException($"{commandName} command takes only {numberOfArguments} " +
                                                   $"argument{(numberOfArguments.ToString().Last() == '1' ? string.Empty : "s")}, " +
                                                   $"{arguments.Length} given.");
            }

            return arguments;
        }

        public void ValidateParameters<T>(string[] arguments, FlagArgument<T>[] commandArguments, string commandName)
        {
            bool skip = false;
            for (var i = 0; i < arguments.Length; i++)
            {
                if (skip)
                {
                    skip = false;
                    continue;
                }

                var argument = arguments[i];
                if (!commandArguments.Where(x => x.Flag == argument).Any())
                {
                    throw new InvalidArgumentException($"Invalid argument {argument} for command {commandName}");
                }

                var command = commandArguments.Where(x => x.Flag == argument).First();
                if (command.PossibleSubArguments.Length == 0)
                {
                    command.Exists = true;
                }
                else if (command.PossibleSubArguments.Length != 0 && command.PossibleSubArguments.Contains(arguments[i + 1]))
                {
                    command.Exists = true;
                    command.CurrentSubArgument = arguments[i + 1];
                    skip = true;
                }
                else
                {
                    throw new InvalidArgumentException($"Invalid argument {argument} {arguments[i + 1]} for command {commandName}");
                }
            }
        }
    }
}
