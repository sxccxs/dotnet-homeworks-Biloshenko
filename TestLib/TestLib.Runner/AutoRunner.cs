using System.Reflection;
using TestLib.Abstractions.Runners;

namespace TestLib.Runner
{
    public class AutoRunner
    {
        public void Run()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var runner = new TestsRunner();
            foreach (var assembly in assemblies)
            {
                runner.Run(assembly);
            }
        }
    }
}