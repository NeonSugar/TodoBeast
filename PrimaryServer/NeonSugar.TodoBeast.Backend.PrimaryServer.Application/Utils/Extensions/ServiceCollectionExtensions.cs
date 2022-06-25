using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Behaviors;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Extensions;
public static class ServiceCollectionExtensions 
{
	public static IServiceCollection AddApplication
		(this IServiceCollection services) 
	{
		return services
			.AddMediatR(assemblies: Assembly.GetExecutingAssembly())
			.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
			// .AddTransient(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(ValidationBehavior<,>))
			.AddTransient(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(LoggingBehavior<,>));
	}
}