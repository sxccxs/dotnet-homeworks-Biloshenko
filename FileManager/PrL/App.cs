using System.Text;
using BLL.Abstractions.Interfaces;

namespace PrL
{
    public class App
    {
        private readonly IParserService parserService;

        public App(IParserService parserService)
        {
            this.parserService = parserService;
        }

        public void RunApp()
        {
            this.Configure();

            while (true)
            {
                Console.Write($"{Directory.GetCurrentDirectory()}> ");
                var input = Console.ReadLine();
                try
                {
                    var res = this.parserService.Parse(input);
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

        private void Configure()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Directory.SetCurrentDirectory("/");
        }
    }
}
