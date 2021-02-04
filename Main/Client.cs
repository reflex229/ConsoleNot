using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Main
{
    public class Client
    {
        public static void Start()
        {
            CreateHostBuilder().Build().Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}