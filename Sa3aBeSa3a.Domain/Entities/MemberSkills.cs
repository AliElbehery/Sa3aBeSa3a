using Sa3aBeSa3a.Domain.Enums;
namespace Sa3aBeSa3a.Domain.Entities
{
    public class MemberSkills
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Members Member { get; set; }
        public int SkillId { get; set; }
        public Skills Skill { get; set; }
        public Profesioncylevel ProficiencyLevel { get; set; } 
    }
}
