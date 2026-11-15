using Sa3aBeSa3a.Domain.Enums;

namespace Sa3aBeSa3a.Domain.Dtos
{
    public class MemberDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public MemberRole Role { get; set; }

    }
}
