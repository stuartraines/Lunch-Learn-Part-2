using System;

namespace Demo.Audit.DynamicProxy
{
    /// <summary>
    /// Use to avoid logging specific operations, parameters or return values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Event | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = true, AllowMultiple = false)]
    public sealed class AuditIgnoreAttribute : Attribute
    {
    }    
}