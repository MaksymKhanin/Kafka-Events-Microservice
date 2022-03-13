using System.ComponentModel.DataAnnotations;

namespace Kafka
{
    public class KafkaTopicsOptions
    {
        [Required]
        public string PayloadTopic { get; set; } = string.Empty;
    }
}
