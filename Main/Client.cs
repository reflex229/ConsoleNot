using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Main
{
    public class Client
    {
        public static void Start(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        //TODO: Fix exception with exiting by Ctrl+C.
    }
}