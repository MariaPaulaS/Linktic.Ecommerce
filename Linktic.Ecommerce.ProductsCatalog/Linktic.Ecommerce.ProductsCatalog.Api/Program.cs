using Linktic.Ecommerce.ProductsCatalog.Api;
using Linktic.Ecommerce.ProductsCatalog.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

public static class Program
{
    public static void Main(string[] args)
    {
        Log.Information("Start Running Products Catalog Lambda");
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var pathToContentRoot = AppDomain.CurrentDomain.BaseDirectory;
        var configurationBuilder = new ConfigurationBuilder();
        _ = configurationBuilder.ConfigureLocalStack();
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