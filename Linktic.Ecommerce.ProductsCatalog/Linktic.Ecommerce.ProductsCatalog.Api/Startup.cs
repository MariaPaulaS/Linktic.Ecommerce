using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Linktic.Ecommerce.ProductsCatalog.Api;

public class Startup
{
    private IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        ConfigurationUtils.Initialize(Configuration);
        IoCServiceCollection.ConfigureServices(services, Configuration);
        ConfigureLogging();
        services.AddCors(o => o.AddPolicy("AllowCorsPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }));
        services.AddControllers().AddNewtonsoftJson();
        services.AddHttpClient();
        services.AddLogging();
        services.AddMvc();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseSerilogRequestLogging();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
    }
    
    private static void ConfigureLogging()
    {
        var levelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = LogEventLevel.Information
                .ToString()
                .Equals(ConfigurationUtils.LoggingLevel)
                ? LogEventLevel.Information
                : LogEventLevel.Error
        };
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel
            .ControlledBy(levelSwitch)
            .Enrich.With(new ThreadIdEnricher())
            .WriteTo.Console(
                outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}