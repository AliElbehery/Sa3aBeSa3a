using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class HelpRequests
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int MemberId { get; set; }
        public Members Member { get; set; } = null!;
        public int CategoryId { get; set; }
        public Categories Category { get; set; } = null!;
        public decimal HoursNeeded { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime NeededBy { get; set; }
        public HelpRequestStatus Status { get; set; } // e.g., Open, In Progress, Completed
        public string District { get; set; } = null!;   
        public ICollection<Offers> Offers { get; set; } = new List<Offers>();
    }
}
