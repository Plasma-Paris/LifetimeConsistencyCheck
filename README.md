![logo](https://github.com/Plasma-Paris/LifetimeConsistencyCheck/raw/master/ReadMe_Resources/logo.png)

# LifetimeConsistencyCheck

Add extensions that allow consistency lifetime check for services registered into Microsoft's native dependency injection system for .NET Core 2.0

## Why we need this ?

Logically, a Scoped or Transient dependency can not be injected into a Singleton dependency: the lifetime of a dependency injected into a service's constructor can not be shorter than the lifetime of the service itself .

Unlike other dependency injection systems, the native microsoft system does not check this behavior. A Scoped dependency injected into a Singleton will behave as if it was itself a Singleton, without the developer being informed.

This can create unwanted edge effects, especially in multi-threaded applications.

This behavior is clearly described in the Microsoft documentation:
![log_warning](https://github.com/Plasma-Paris/LifetimeConsistencyCheck/raw/master/ReadMe_Resources/doc_warning.png)
Source : https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.0

## Usage

```c#
public class Startup
{
	// …

	public IServiceProvider ConfigureServices(IServiceCollection services)
	{
		// …
		services.AddSingleton<IMyService1, MyService1>();
		services.AddScoped<IMyService2, MyService2>();
		// …

		services.CheckLifetimeConsistency();
		return services.BuildServiceProviderWithLifetimeConsistencyCheck(true);
	}
}
```

### Check at application start

```c#
services.CheckLifetimeConsistency();
```
The first method checks the consistency of the lifetime of all the services that were register (as far as possible) as soon as the method is executed, which makes it possible to be informed as soon as possible if there is an inconsistency.

### Check at service resolving

```c#
services.BuildServiceProviderWithLifetimeConsistencyCheck(true);
```
The second method checks the consistency of each service regarding its dependencies at its resolving.

### Why two differtents methods ?

Ideally we would have wanted to use only the first method, but it is not always possible. If a service is register as follows:

```c#
services.AddSingleton<IMyService>((c) => new MyService(c.GetService<IMyService2>());
```

It is possible to know the exact implementation of the service only by running the factory that creates it. For avoid undesirable edge effects, check of the consistency of this kind of service is done only when the dependency injection system resolves the service.

### To Do

* **PUSH SOURCES !!** :-)
* Create a Nuget package
* Ignore unresolvale constructors
* Optimze the chek at resolve
* Make the error message more detailed when chek at resolve
* …

## Contributing

If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are welcome.

## Licensing

The code in this project is licensed under MIT license.