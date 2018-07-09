using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using DHX.Gantt.Models;

namespace DHX.Gantt
{
    public class Program
    {
     
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .InitializeDatabase()
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
