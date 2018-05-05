using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skillx.Communication.ServiceBus.Abstractions;
using Skillx.Modules.User.Config;
using Skillx.Modules.User.Core.Repositories;
using Skillx.Modules.User.Core.Services;
using Skillx.Modules.User.Data.Repositories;
using Skillx.Modules.User.Impl.Services;

namespace Skillx.Modules.User
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

            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
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
