using Serilog;

namespace IncomeTaxApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occurred.");
                throw;
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder =
                Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration(config =>
                    {
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                    .UseDefaultServiceProvider((context, options) =>
                    {
                        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                        options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment();
                    });

            return builder;
        }
    }
}
