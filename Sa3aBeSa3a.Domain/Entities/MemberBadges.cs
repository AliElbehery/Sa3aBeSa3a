using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class MemberBadges
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Members Member { get; set; }
        public int BadgeId { get; set; }
        public Badges Badge { get; set; }
    }
}
