using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Linktic.Ecommerce.OrdersManagement.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace Linktic.Ecommerce.OrdersManagement.Api;

public class LambdaEntryPoint : ApplicationLoadBalancerFunction
{
    protected override void Init(IHostBuilder builder)
    {
        var pathToContentRoot = AppDomain.CurrentDomain.BaseDirectory;
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.ConfigureParameterStore();
        var root = configurationBuilder.Build();
        
        builder
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