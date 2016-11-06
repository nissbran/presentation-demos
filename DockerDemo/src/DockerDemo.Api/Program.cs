namespace DockerDemo.Api
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
           var webhost = new WebHostBuilder()
            .UseKestrel()
            .UseUrls("http://*:5000")
            .UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .Build();

            webhost.Run();
        }
    }
}
