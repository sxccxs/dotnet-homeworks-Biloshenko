using BLL.Abstractions.Interfaces;
using System.Text;

namespace PrL
{
    public class App

    {
        private readonly IParserService _parserService;
        public App(IParserService parserService)
        {
            _parserService = parserService;
        }

        private void Config()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Directory.SetCurrentDirectory("/");
        }

        public void RunApp()
        {
            Config();

            while (true)
            {
                Console.Write($"{Directory.GetCurrentDirectory()}> ");
                var inp = Console.ReadLine();
                try
                {
                    var res = _parserService.Parse(inp);
                    if (res.HasValue)
                    {
                        Console.WriteLine();
                        Console.WriteLine(res.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
