using System;

namespace Demo.Audit.DynamicProxy
{
    [AttributeUsage(AttributeTargets.Event | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public class EventTypeAttribute : Attribute
    {
        public EventTypeAttribute(string eventType)
        {
            EventType = eventType;
        }

        public string EventType { get; set; }
    }
}