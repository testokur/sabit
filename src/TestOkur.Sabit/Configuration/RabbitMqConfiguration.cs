using System;
using System.ComponentModel.DataAnnotations;

namespace TestOkur.Sabit.Configuration
{
    public class RabbitMqConfiguration
    {
        [Required]
        public string Hostname { get; set; }
        [Required]
        public ushort Port { get; set; }
        [Required]
        public string Vhost { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public Uri ToUri()
        {
            return new Uri($@"amqp://{Username}:{Password}@{Hostname}:{Port}/{Vhost}");
        }
    }
}