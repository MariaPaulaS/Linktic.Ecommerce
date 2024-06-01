using DotNet.Testcontainers.Builders;
using IContainer = DotNet.Testcontainers.Containers.IContainer;

namespace Linktic.Ecommerce.ProductsCatalog.Api;

public class LocalStackContainer
{
    private readonly IContainer _container;
    public static string GetConnectionString() => "http://localhost:4566";

    public LocalStackContainer()
    {
        var localStackBuilder = new ContainerBuilder()
            .WithImage("localstack/localstack:1.3.1")
            .WithCleanUp(true)
            .WithEnvironment("DEFAULT_REGION", "us-east-1")
            .WithEnvironment("SERVICES", "dynamodb")
            .WithEnvironment("DOCKER_HOST", "unix:///var/run/docker.sock")            
            .WithEnvironment("DEBUG", "1")
            .WithPortBinding(4566, 4566);

        _container = localStackBuilder.Build();
    }
    
    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }
    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
    
}