using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class Reviews
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public Members Reviewer { get; set; } = null!;
        public int RevieweeId { get; set; }
        public Members Reviewee { get; set; } = null!;
        public int Rating { get; set; } // 1 to 5
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int HelpRequestId { get; set; }
        public HelpRequests HelpRequest { get; set; } = null!;

    }
}
