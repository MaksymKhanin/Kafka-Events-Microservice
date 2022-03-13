using Core.Payload;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration
{
    public static class Dependencies {
        public static PayloadValidationBuilder AddPayloadOutgoing(
                this IServiceCollection serviceCollection,
                IConfiguration configuration)
        {   
            serviceCollection.AddTransient<IPayloadValidator, PayloadValidator>();

            return new PayloadValidationBuilder(serviceCollection, configuration);
        }
    }
   
    public record PayloadValidationBuilder(
        IServiceCollection Services,
        IConfiguration Configuration);
}
