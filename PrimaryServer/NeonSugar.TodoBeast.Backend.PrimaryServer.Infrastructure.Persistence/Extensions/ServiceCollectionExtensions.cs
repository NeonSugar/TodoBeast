using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.Contexts;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.UowRepositories;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.Extensions;
public static class ServiceCollectionExtensions 
{
	public static IServiceCollection AddPersistence
		(this IServiceCollection services, string connectionString) 
	{
		return services
			.AddScoped<IUnitOfWork, UnitOfWork>()
			.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
	}
}
