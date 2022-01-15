using BLL;
using Microsoft.Extensions.DependencyInjection;

namespace PrL
{
    public static class Program
    {
        public static void Main()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<App>()?.RunApp();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<App>();
            DependencyRegistrar.ConfigureServices(services);
        }
    }
}