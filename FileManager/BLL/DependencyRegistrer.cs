using BLL.Abstractions.Interfaces;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class DependencyRegistrer
    {
        public static void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IParserService, CommandParserService>();
        }
    }
}
