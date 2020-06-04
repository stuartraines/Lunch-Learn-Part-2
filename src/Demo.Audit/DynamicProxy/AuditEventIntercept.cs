using Audit.Core;
using Newtonsoft.Json;

namespace Demo.Audit.DynamicProxy
{
    /// <summary>
    /// Represents the output of the audit process for the Audit.DynamicProxy
    /// </summary>
    public class AuditEventIntercept : AuditEvent
    {
        /// <summary>
        /// Gets or sets the intercepted event details.
        /// </summary>
        [JsonProperty(Order = 10)]
        public InterceptEvent InterceptEvent { get; set; }
    }    
}