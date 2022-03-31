using System.ComponentModel.DataAnnotations;

namespace OpenExchange.Models
{

    public class EUR
    {
        [Key]
        [Required, StringLength(36)]
        public string EurId { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Average { get; set; }
    }
}
