using System;

namespace Microsoft.Extensions.DependencyInjection;

[Serializable]
public class InconsistentServicesException : Exception
{
    public InconsistentService[] InsaneServices { get; }
    public int ErrorCount => InsaneServices.Length;
    public InconsistentServicesException(params InconsistentService[] insaneServices)
        : this($"One or more services are not in a valid state. See {nameof(InconsistentService)} property for errors descriptions", insaneServices)
    {
        InsaneServices = insaneServices;
    }
    public InconsistentServicesException(string message, params InconsistentService[] insaneServices)
        : base(message)
    {
        InsaneServices = insaneServices;
    }
    protected InconsistentServicesException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
