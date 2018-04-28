using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Skillx.Modules.Auth.Data;
using System;
using System.Reflection;

namespace Skillx.Modules.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<SkillxAuthContext>();

                context.Database.EnsureCreated();

                host.Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup(Assembly.GetExecutingAssembly().FullName)
                .Build();
    }
}
