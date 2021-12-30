using Core.Dataclasses;
using Core.Exceptions;

namespace BLL.Utils
{
    internal class ArgumentsValidator
    {
        public string[] ValidateNArguments(string[] args, int n, string cmdName)
        {
            if (args is null || args?.Length < n)
            {
                throw new InvalidArgumentException($"{cmdName} command needs {n} argument" +
                                                   $"{(n.ToString().Last() == '1' ? "" : "s")}," +
                                                   $" {args.Length} given.");
            }
            else if (args?.Length > n)
            {

                throw new InvalidArgumentException($"{cmdName} command takes only {n} " +
                                                   $"argument{(n.ToString().Last() == '1' ? "" : "s")}, " +
                                                   $"{args.Length} given.");
            }
            return args;

        }
        public void ValidateParameters<T>(string[] args, FlagArgument<T>[] cmdArgs, string cmdName)
        {
            bool skip = false;
            for (var i = 0; i < args.Length; i++)
            {
                if (skip)
                {
                    skip = false;
                    continue;
                }
                var arg = args[i];
                if (!cmdArgs.Where(x => x.Flag == arg).Any())
                {
                    throw new InvalidArgumentException($"Invalid argument {arg} for command {cmdName}");
                }
                var cmd = cmdArgs.Where(x => x.Flag == arg).First();
                if (cmd.PossibleSubArguments.Length == 0)
                {
                    cmd.Exists = true;
                }
                else if (cmd.PossibleSubArguments.Length != 0 && cmd.PossibleSubArguments.Contains(args[i + 1]))
                {
                    cmd.Exists = true;
                    cmd.CurrentSubArgument = args[i + 1];
                    skip = true;
                }
                else
                {
                    throw new InvalidArgumentException($"Invalid argument {arg} {args[i + 1]} for command {cmdName}");
                }
            }
        }
    }
}
