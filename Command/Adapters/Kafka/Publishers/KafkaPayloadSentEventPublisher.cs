using Application.Ports;
using Common;
using Confluent.Kafka;
using Core.Events;
using Kafka.Topics;
using Microsoft.Extensions.Logging;

namespace Kafka.Publishers
{
    internal class KafkaPayloadSentEventPublisher : IPayloadSentEventPublisher
    {
        private readonly IProducer<string, PayloadSentEvent> _producer;
        private readonly PayloadKafkaTopic _topicDefinition;
        private readonly ILogger<KafkaPayloadSentEventPublisher> _logger;

        public KafkaPayloadSentEventPublisher(IProducer<string, PayloadSentEvent> successProducer, PayloadKafkaTopic topicDefinition, ILogger<KafkaPayloadSentEventPublisher> logger)
        {
            _producer = successProducer;
            _topicDefinition = topicDefinition;
            _logger = logger;
        }

        public async Task Publish(PayloadSent @event)
        {
            _logger.LogDebug("An event of type {DomainEventType} is about to be published in the topic {Topic} {CorrelationId}", nameof(PayloadSent), _topicDefinition.Topic, @event.TicketId);

            var message = new Message<string, PayloadSentEvent>
            {
                Key = @event.TicketId.ToString(),
                Value = new() { ticketId = @event.TicketId },
                Headers = new Headers()
            };
            //message.Headers.Add(KafkaConstants.MessageHeaderNames.EventType, Encoding.UTF8.GetBytes(typeof(PayloadSentEvent).FullName!));
            //message.Headers.Add(KafkaConstants.MessageHeaderNames.CorrelationId, @event.TicketId.ToByteArray());

            try
            {
                var result = await _producer.ProduceAsync(_topicDefinition.Topic, message);

                _logger.LogDebug("An event of type {DomainEventType} has been processed with final status {PersistenceStatus} {CorrelationId}", typeof(PayloadSent).Name, result.Status.ToString(), @event.TicketId);
            }
            catch (ProduceException<string, PayloadSentEvent> e)
            {
                _logger.LogError(e, "An event of type {DomainEventType} could not be published to the topic {Topic} {CorrelationId}", nameof(PayloadSent), _topicDefinition.Topic, @event.TicketId);

                throw;
            }
        }
    }
}
