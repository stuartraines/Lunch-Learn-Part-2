using System;

namespace Demo.Api.Models
{
    public class AuditSearchRequest
    {
        public int From { get; set; } = 0;

        public int Size { get; set; } = 10;
        
        public string EventType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}