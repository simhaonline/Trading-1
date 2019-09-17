using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace vITGrid.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ILog log = Log.Logger.CreateIntance(@"c:\temp", "log.txt", ErrorType.Info);
            //log.Info("vITGrid.Spa Starting");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}