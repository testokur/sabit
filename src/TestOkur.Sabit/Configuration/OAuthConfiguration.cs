using System.ComponentModel.DataAnnotations;

namespace TestOkur.Sabit.Configuration
{
    public class OAuthConfiguration
    {
        [Required]
        public string Authority { get; set; }

        [Required]
        public bool RequireHttpsMetadata { get; set; }

        [Required]
        public string ApiName { get; set; }
    }
}