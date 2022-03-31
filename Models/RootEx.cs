using System.ComponentModel.DataAnnotations;

namespace OpenExchange.Models
{
    public class RootEx
    {
        [Key]
        [Required, StringLength(36)]
        public string RootExId { get; set; }
        public string Disclaimer { get; set; }
        public string License { get; set; }
        public int Timestamp { get; set; }
        public string @base { get; set; }
        public RatesEx Rates { get; set; }
    }
}
