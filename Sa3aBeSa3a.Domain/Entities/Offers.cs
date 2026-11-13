using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class Offers
    {
        public int Id { get; set; }
        public int HelpRequestId { get; set; }
        public HelpRequests HelpRequest { get; set; }
        public int MemberId { get; set; }
        public Members Member { get; set; }
        public decimal OfferedHours { get; set; }
        public DateTime OfferDate { get; set; }
        public OfferStatus Status { get; set; } // e.g., Pending, Accepted, Rejected
        public string Message { get; set; } // Optional message from the member making the offer
    }
}
