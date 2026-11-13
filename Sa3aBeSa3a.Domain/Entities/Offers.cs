using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class Offers
    {
        public int Id { get; set; }
        public int HelpRequestId { get; set; }
        public HelpRequests HelpRequest { get; set; }
        public int MemberId { get; set; }
        public Members Member { get; set; }
        public decimal OfferedHours { get; set; }

    }
}
