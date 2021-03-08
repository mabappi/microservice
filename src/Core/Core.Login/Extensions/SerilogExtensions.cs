using Microsoft.Extensions.Hosting;
using Serilog;

namespace Core.Login.Extensions
{
    public static class SerilogExtensions
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithProperty("ServiceName", "Login Frontend")
                .WriteTo.Seq("http://seq:5341")
                .CreateLogger();
            return hostBuilder;
        }
    }
}
