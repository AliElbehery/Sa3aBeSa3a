using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class MemberBadges
    {
        public int MemberId { get; set; }
        public Members Member { get; set; } = null!;
        public int BadgeId { get; set; }
        public Badges Badge { get; set; } = null!;
    }
}
