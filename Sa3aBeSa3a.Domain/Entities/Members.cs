

namespace Sa3aBeSa3a.Domain.Entities
{
    public class Members
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinedDate { get; set; } 
        public ICollection<LedgerEntries> LedgerEntries { get; set; }
        public ICollection<MemberSkills> MemberSkills { get; set; }
        public ICollection<MemberBadges> MemberBadges { get; set; }
        public ICollection<HelpRequests> HelpRequests { get; set; }
        public ICollection<Offers> Offers { get; set; }
    }
}
