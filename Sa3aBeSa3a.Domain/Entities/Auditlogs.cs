using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Entities
{
    public class AuditLogs
    {
        public int Id { get; set; }
        public string Actor { get; set; } = null!;
        public string Action { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string Source { get; set; } = null!;
        public string Target { get; set; } = null!;

    }
}
