using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using static Web.LangInfo;

namespace Web
{
    public static class Program
    {
        public static string Port { get; set; }

        public static void Main(string[] args)
        {   
            EncodingFix();
            Port = "5947";

            try
            {
                for (var i = 0; i < args.Length; i++)
                    switch (args[i])
                    {
                        case "--help":
                            Console.WriteLine(ResourceManagerProp.GetString("Commands_Help_", CultureInfoProp));
                            return;
                        case "--port":
                            Port = args[i + 1];
                            break;
                    }
            }
            catch
            {
                Console.WriteLine(ResourceManagerProp.GetString("There_is_no_", CultureInfoProp));
            }

            CreateHostBuilder(args).Build().Run();
        }
        
        
        private static void EncodingFix()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.InputEncoding = Encoding.Unicode;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
            }
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}