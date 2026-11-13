using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class HelpRequests
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MemberId { get; set; }
        public Members Member { get; set; }
        public int SkillId { get; set; }
        public Skills Skill { get; set; }
        public decimal HoursNeeded { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
