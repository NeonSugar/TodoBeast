using Microsoft.AspNetCore.Builder;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Middleware;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Extensions;
internal static class ServiceCollectionExtensions 
{
	internal static IApplicationBuilder UseCustomExceptionHandler
		(this IApplicationBuilder builder)
	{
		return builder
			.UseMiddleware<CustomExceptionHandlerMiddleware>();
	}
}