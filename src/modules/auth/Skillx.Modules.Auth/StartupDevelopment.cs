using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skillx.Communication.ServiceBus.Options;
using Skillx.Modules.Auth.Constants;
using Skillx.Modules.Auth.Data;

namespace Skillx.Modules.Auth
{
    public class StartupDevelopment : BaseStartup
    {
        public StartupDevelopment(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            var rabbitMQConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQUrl);
            services.Configure<RabbitMQOptions>(opts => opts.ConnectionString = rabbitMQConnectionString);

            var dbConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariables.SqlServerConnection);
            services.AddDbContext<SkillxAuthContext>(opts => opts.UseSqlServer(dbConnectionString));
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            base.Configure(app, env);
        }
    }
}
