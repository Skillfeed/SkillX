﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skillx.Communication.ServiceBus;
using Skillx.Communication.ServiceBus.Abstractions;
using Skillx.Communication.ServiceBus.Options;
using Skillx.Modules.Courses.Constants;
using Skillx.Modules.Courses.Core.Options;

namespace Skillx.Modules.Courses
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
            services.AddSingleton<IMessageBus, RabbitMQMessageBus>();

            services.Configure<DatabaseOptions>(opts =>
            {
                opts.ConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariables.MongoConnectionString);
                opts.DatabaseName = "Skillx_Courses";
            });
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            base.Configure(app, env);
        }
    }
}
