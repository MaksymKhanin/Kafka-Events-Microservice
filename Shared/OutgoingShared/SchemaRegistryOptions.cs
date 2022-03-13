using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class SchemaRegistryOptions
    {
        [Required]
        public string SchemaRegistryUrl { get; set; } = string.Empty;

        [Required]
        public string SchemaRegistryApiKeyId { get; set; } = string.Empty;

        [Required]
        public string SchemaRegistryApiKeySecret { get; set; } = string.Empty;
    }
}
