using System.ComponentModel.DataAnnotations;

namespace TestOkur.Sabit.Configuration
{
    public class AppConfiguration
    {
        [Required]
        public int CacheDurationSec { get; set; }
        [Required]
        public RabbitMqConfiguration RabbitMq { get; set; }
    }
}
