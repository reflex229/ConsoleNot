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
    }
}
/*TODO:
var client = new RestClient("http://localhost:5001/Not/Title-Description-1-1");
client.Timeout = -1;
var request = new RestRequest(Method.POST);
IRestResponse response = client.Execute(request);
Console.WriteLine(response.Content);
*/