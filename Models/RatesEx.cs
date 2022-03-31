using System;
using System.ComponentModel.DataAnnotations;

namespace OpenExchange.Models
{
    public class RatesEx
    {
        [Key]
        [Required, StringLength(36)]
        public string RatestId { get; set; }
        public double Eur { get; set; }
        public double Gbp { get; set; }
        public double Rsd { get; set; }
        public DateTime DateRate { get; set; }
    }
}
