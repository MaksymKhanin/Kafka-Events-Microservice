using System.ComponentModel.DataAnnotations;

namespace Kafka
{
    public class KafkaBrokerOptions
    {
        [Required]
        public string BrokerBootstrapServers { get; set; } = string.Empty;

        [Required]
        public string BrokerUsername { get; set; } = string.Empty;

        [Required]
        public string BrokerPassword { get; set; } = string.Empty;

        [Required]
        public string BrokerSecurityProtocol { get; set; } = "SaslSsl";
    }
}
