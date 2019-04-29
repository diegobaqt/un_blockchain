namespace UNBlockchain.Models
{
    public class TransactionVm
    {
        public string ToAddress { get; set; }

        public string Secret { get; set; }

        public decimal Amount { get; set; }
    }
}
