using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skillx.Gateways.WebAPI.Attributes;
using Skillx.Gateways.WebAPI.Services.Abstraction;
using Skillx.Gateways.WebAPI.Services.Abstraction.Common;
using Skillx.Gateways.WebAPI.Services.Implementation;
using Skillx.Gateways.WebAPI.Services.Implementation.Common;

namespace Skillx.Gateways.WebAPI
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
            services.AddMvc();
            services.AddOptions();

            services.AddScoped<AuthorizeAsyncAttribute>();

            services.AddTransient<IApplicationHttpClient, ApplicationHttpClient>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICourseService, CourseService>();
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller}/{action}");
            });
        }
    }
}
