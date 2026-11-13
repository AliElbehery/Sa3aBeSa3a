using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class LedgerEntries
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Members Member { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; }
    }
}
