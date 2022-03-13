using Application.Ports;
using Common;
using Common.Configuration;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Core.Configuration;
using Kafka.Publishers;
using Kafka.Topics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Kafka.Configuration
{
    public static class Dependencies
    {
        public static PayloadValidationBuilder AddKafkaProducer(this PayloadValidationBuilder builder)
        {
            builder.Services.AddSingleton<IPayloadSentEventPublisher, KafkaPayloadSentEventPublisher>();

            builder.Services.AddOptions<KafkaBrokerOptions>()
                .Bind(builder.Configuration.GetSection("Kafka"))
                .ValidateDataAnnotations();

            builder.Services.AddOptions<KafkaTopicsOptions>()
                .Bind(builder.Configuration.GetSection("Kafka"))
                .ValidateDataAnnotations();

            builder.Services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<KafkaTopicsOptions>>().Value;

                return new PayloadKafkaTopic(options.PayloadTopic);
            });

            // Broker Producer config setup
            builder.Services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<KafkaBrokerOptions>>().Value;

                return new ProducerConfig
                {
                    BootstrapServers = options.BrokerBootstrapServers

                    //SecurityProtocol = Enum.Parse<SecurityProtocol>(options.BrokerSecurityProtocol, true),
                    //SaslMechanism = SaslMechanism.,    // Plain (Username/pass). Do not misunderstand with PLAINTEXT !!!
                    //SaslUsername = options.BrokerUsername,
                    //SaslPassword = options.BrokerPassword,
                };
            });

            builder.Services.AddKafkaSchemaRegistryClient();

            builder.Services.AddKafkaProducer<PayloadSentEvent>();

            return builder;
        }

        private static IServiceCollection AddKafkaProducer<TEvent>(this IServiceCollection services) =>
            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<ProducerConfig>();
                var builder = new ProducerBuilder<string, TEvent>(config);
                var srClient = sp.GetRequiredService<ISchemaRegistryClient>();
                var valueSerializer = new AvroSerializer<TEvent>(srClient,
                    new AvroSerializerConfig
                    {
                        SubjectNameStrategy = SubjectNameStrategy.TopicRecord,
                        AutoRegisterSchemas = false
                    });

                builder.SetValueSerializer(valueSerializer);

                return builder.Build();
            });
    }
}
