using System.ComponentModel.DataAnnotations;

namespace OpenExchange.Models
{

    public class Rates
    {
        [Key]
        [Required, StringLength(36)]
        public string RatestId { get; set; }
        public EUR EUR { get; set; }
        public GBP GBP { get; set; }
    }
}
