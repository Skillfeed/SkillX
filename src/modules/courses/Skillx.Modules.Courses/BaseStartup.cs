using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skillx.Modules.Courses.Attributes.Filters.Action;
using Skillx.Modules.Courses.Core;
using Skillx.Modules.Courses.Core.Repositories;
using Skillx.Modules.Courses.Data.Repositories;
using Skillx.Modules.Courses.Impl;

namespace Skillx.Modules.Courses
{
    public class BaseStartup
    {
        private readonly IConfiguration configuration;

        public BaseStartup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected IConfiguration Configuration => this.configuration;

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opts =>
            {
                opts.Filters.Add(new ModelStateValidationAttribute());
            });
            services.AddAutoMapper();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddTransient<ICourseManager, CourseManager>();
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller}/{action}");
            });
        }
    }
}
