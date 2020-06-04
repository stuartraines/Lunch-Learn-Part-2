using System;

namespace Demo.Api.Models
{
    public class AuditEventDescriptor
    {
        public Guid AuditEventId { get; set; }

        public string Index { get; set; }

        public string EventType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Duration { get; set; }

        public bool Success { get; set; }        
    }
}