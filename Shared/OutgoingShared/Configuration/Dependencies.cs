using Confluent.SchemaRegistry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddKafkaSchemaRegistryClient(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddOptions<SchemaRegistryOptions>()
                .BindConfiguration("Kafka")
                .ValidateDataAnnotations();

            serviceCollection
                .AddSingleton<ISchemaRegistryClient>(sp =>
                {
                    var options = sp.GetRequiredService<IOptions<SchemaRegistryOptions>>().Value;

                    return new CachedSchemaRegistryClient(new Dictionary<string, string>
                    {
                        { SchemaRegistryConfig.PropertyNames.SchemaRegistryUrl, options.SchemaRegistryUrl },
                        {
                            SchemaRegistryConfig.PropertyNames.SchemaRegistryBasicAuthCredentialsSource, "USER_INFO"
                        },
                        {
                            SchemaRegistryConfig.PropertyNames.SchemaRegistryBasicAuthUserInfo,
                            $"{options.SchemaRegistryApiKeyId}:{options.SchemaRegistryApiKeySecret}"
                        }
                    });
                });

            return serviceCollection;
        }
    }
}
