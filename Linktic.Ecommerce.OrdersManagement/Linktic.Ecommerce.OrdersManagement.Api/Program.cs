using Linktic.Ecommerce.OrdersManagement.Api;
using Linktic.Ecommerce.OrdersManagement.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

public static class Program
{
    public static void Main(string[] args)
    {
        Log.Information("Start Running Orders Management Lambda");
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var pathToContentRoot = AppDomain.CurrentDomain.BaseDirectory;
        var configurationBuilder = new ConfigurationBuilder();
        _ = configurationBuilder.ConfigureLocalRunning();
        var root = configurationBuilder.Build();

        return Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureAppConfiguration((_, builder) =>
            {
                builder.SetBasePath(pathToContentRoot);
                builder.AddConfiguration(root);
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseKestrel()
                    .UseIISIntegration()
                    .UseStartup<Startup>();
            });
    }
}