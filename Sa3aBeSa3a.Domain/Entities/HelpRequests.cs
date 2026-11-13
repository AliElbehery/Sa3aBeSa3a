using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class HelpRequests
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MemberId { get; set; }
        public Members Member { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
        public decimal HoursNeeded { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime NeededBy { get; set; }
        public HelpRequestStatus Status { get; set; } // e.g., Open, In Progress, Completed
        public string District { get; set; }
        public ICollection<Offers> Offers { get; set; } = new List<Offers>();
    }
}
