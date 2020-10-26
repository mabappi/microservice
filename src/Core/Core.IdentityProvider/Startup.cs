using System;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;

namespace Core.IdentityProvider
{
    public class Startup
    {
        private const string CorsPolicyName = "CorsPolicy";

        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<ForwardedHeadersOptions>(options => {
                    options.ForwardedHeaders = ForwardedHeaders.All;
                });
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"/dataprotection"))
                .SetApplicationName("ApplicationCookie");
            services.Configure<CookiePolicyOptions>(options => {
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });
            services
                .Configure<CookieAuthenticationOptions>(SetCookieOptions)
                .ConfigureApplicationCookie(SetCookieOptions);
            services.AddCors(x => x.AddPolicy(CorsPolicyName, builder => builder.SetIsOriginAllowed(host => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            services.AddIdentityServer(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseForwardedHeaders()
                .UseCors(CorsPolicyName)
                .UseIdentityServer()
                .UseAuthorization()
                .UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        private void SetCookieOptions(CookieAuthenticationOptions options)
        {
            options.Cookie.Path = "/";
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            if (!string.IsNullOrEmpty(Configuration["DomainName"]))
            {
                options.Cookie.Domain = Configuration["DomainName"];
            }
        }

    }
}
