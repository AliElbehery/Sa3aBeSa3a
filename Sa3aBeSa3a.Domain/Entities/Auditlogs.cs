using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class Auditlogs
    {
        public int Id { get; set; }
        public string actor { get; set; }
        public string Action { get; set; }
        public DateTime CreateAt { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }

    }
}
