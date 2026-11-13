using Sa3aBeSa3a.Domain.Enums;
namespace Sa3aBeSa3a.Domain.Entities
{
    public class MemberSkills
    {
        public int MemberId { get; set; }
        public Members Member { get; set; } = null!;
        public int SkillId { get; set; }
        public Skills Skill { get; set; } = null!;
        public Profesioncylevel ProficiencyLevel { get; set; } 
    }
}
