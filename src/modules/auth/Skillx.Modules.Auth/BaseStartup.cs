using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Skillx.Communication.ServiceBus;
using Skillx.Communication.ServiceBus.Abstractions;
using Skillx.Modules.Auth.Attributes.Filters.Action;
using Skillx.Modules.Auth.Core.Jwt;
using Skillx.Modules.Auth.Core.Options;
using Skillx.Modules.Auth.Data;
using Skillx.Modules.Auth.Jwt;
using System;
using System.Text;

namespace Skillx.Modules.Auth
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
            services.AddMvc(options =>
            {
                options.Filters.Add(new ModelStateValidationAttribute());
            });
            services.AddOptions();
            services.AddAutoMapper();
            services.AddSingleton<IMessageBus, RabbitMQMessageBus>();

            this.AddAuthenticationServices(services);
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller}/{action}");
            });
        }

        private void AddAuthenticationServices(IServiceCollection services)
        {
            var jwtAppSettingOptions = this.Configuration.GetSection(nameof(JwtIssuerOptions));
            var secret = jwtAppSettingOptions[nameof(JwtIssuerOptions.Secret)];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddIdentity<Core.Entities.User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequiredLength = 3;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;

            })
            .AddEntityFrameworkStores<SkillxAuthContext>();

            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
        }
    }
}
