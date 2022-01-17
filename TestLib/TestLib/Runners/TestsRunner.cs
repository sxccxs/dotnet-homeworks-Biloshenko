using System.Reflection;
using TestLib.Core.Attributes;
using TestLib.Core.Exceptions;

namespace TestLib.Abstractions.Runners
{
    public class TestsRunner
    {
        public void Run(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => type.IsClass &&
                                                          Attribute.GetCustomAttribute(type, typeof(TestClassAttribute)) is not null);

            foreach (var type in types)
            {
                this.RunGroupOfTests(type);
            }
        }

        private void RunGroupOfTests(Type type)
        {
            // Get all methods with needed atribute and which have needed signature
            var methods = type.GetMethods().Where(method => method.IsPublic &&
                                                            !method.IsStatic &&
                                                            method.GetParameters().Length == 0 &&
                                                            method.ReturnType == typeof(void) &&
                                                            Attribute.GetCustomAttribute(method, typeof(TestMethodAttribute)) is not null);

            // Get AfterAll method if it defined
            var afterAll = type.GetMethods().Where(method => method.IsPublic &&
                                                   !method.IsStatic &&
                                                   method.GetParameters().Length == 0 &&
                                                   method.ReturnType == typeof(void) &&
                                                   method.Name == "AfterAll").FirstOrDefault();

            // Get BeforeEach method if it defined
            var beforeEach = type.GetMethods().Where(method => method.IsPublic &&
                                       !method.IsStatic &&
                                       method.GetParameters().Length == 0 &&
                                       method.ReturnType == typeof(void) &&
                                       method.Name == "BeforeEach").FirstOrDefault();

            var testClass = type.GetConstructor(Type.EmptyTypes).Invoke(null);
            Console.WriteLine($"Running tests {type.FullName}");
            Console.WriteLine();
            foreach (var method in methods)
            {
                this.RunTestMethod(testClass, method, beforeEach);
            }

            if (afterAll is not null)
            {
                afterAll.Invoke(testClass, null);
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private void RunTestMethod(object testClass, MethodInfo method, MethodInfo before)
        {
            var prefix = "    ";
            try
            {
                if (before is not null)
                {
                    before.Invoke(testClass, null);
                }

                method.Invoke(testClass, null);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Test {method.Name} PASSED");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (TargetInvocationException ex) when (ex.InnerException is AssertionException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Test {method.Name} FAILED");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"{prefix}{ex.InnerException.Message}");
            }
            catch (TargetInvocationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unexpected exception happened while executing {method.Name}");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"{prefix}{ex.InnerException}");
            }
            finally
            {
                if (testClass is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
