using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class Auditlogs
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
        public Members User { get; set; }
    }
}
