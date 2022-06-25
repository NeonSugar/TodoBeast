using Microsoft.EntityFrameworkCore;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;
using Serilog;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.Contexts;
public sealed class AppDbContext : DbContext  
{
	public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
		: base(contextOptions) 
	{
		if(base.Database.EnsureCreated()) 
		{
			Log.Error(messageTemplate: "No existed database has been found. New database has been created");
		}
	}

	public DbSet<TodoList> ToDoLists { get; set; } = null!;
	public DbSet<TodoItem> ToDoItems { get; set; } = null!;
}
