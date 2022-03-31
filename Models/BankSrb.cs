namespace OpenExchange.Models
{
    public class BankSrb
    {
        public string Code { get; set; }
        public string Date { get; set; }
        public string Date_from { get; set; }
        public int Number { get; set; }
        public int Parity { get; set; }
        public double Cash_buy { get; set; }
        public double Cash_sell { get; set; }
        public double Exchange_buy { get; set; }
        public double Exchange_middle { get; set; }
        public double Exchange_sell { get; set; }
    }
}
