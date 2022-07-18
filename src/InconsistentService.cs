using System;

namespace Microsoft.Extensions.DependencyInjection;

public class InconsistentService
{
    public Type ServiceType { get; set; }
    public ServiceLifetime ServiceLifeTime { get; set; }
    public Type CallingServiceType { get; set; }
    public ServiceLifetime CallingServiceLifeTime { get; set; }
}
