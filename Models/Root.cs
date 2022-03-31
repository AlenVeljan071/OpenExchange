using System;
using System.ComponentModel.DataAnnotations;

namespace OpenExchange.Models
{
    public class Root
    {
        [Key]
        [Required, StringLength(36)]
        public string RootId { get; set; }
        public string Disclaimer { get; set; }
        public string License { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public string @base { get; set; }
        public Rates Rates { get; set; }
    }
}
