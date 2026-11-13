

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
        public decimal TotalHours { get; set; }
        public int ReputationScore { get; set; }
    }
}
