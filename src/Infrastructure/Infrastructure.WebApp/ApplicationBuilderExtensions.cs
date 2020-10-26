using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.WebApp
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePathForwarder(this IApplicationBuilder app, string pathBase)
        {
            app.Use((context, next) => {
                if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    context.Request.PathBase = new PathString(pathBase);
                }
                return next();
            });
            return app;
        }
    }
}