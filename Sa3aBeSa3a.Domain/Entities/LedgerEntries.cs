using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class LedgerEntries
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Members Member { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionTypes TransactionType { get; set; }
    }
}
