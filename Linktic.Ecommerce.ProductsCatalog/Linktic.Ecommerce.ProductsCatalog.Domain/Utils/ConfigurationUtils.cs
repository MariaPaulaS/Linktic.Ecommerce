using Microsoft.Extensions.Configuration;

namespace Linktic.Ecommerce.ProductsCatalog.Domain.Utils;

public class ConfigurationUtils
{
    private static IConfiguration? _configuration;
    private const string ProductsCatalog = "productsCatalog";
    
    public static void Initialize(IConfiguration configuration) => _configuration = configuration;

}