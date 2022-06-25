using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.Contexts;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.UowRepositories.Repositories.Base;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.UowRepositories.Repositories;
internal sealed class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository 
{
	public TodoItemRepository(AppDbContext context) : base(context) 
	{
		// empty
	}

	public Task<List<TodoItem>> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
	{
		return Entities
			.Where(item => item.Id.Equals(id))
			.Include(p => p.TodoList)
			.ToListAsync(cancellationToken);
	}
	public Task<List<TodoItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken) 
	{
		return Entities
			.Where(item => item.TodoList.UserId.Equals(userId))
			.Include(p => p.TodoList)
			.ToListAsync(cancellationToken);
	}
	public Task<List<TodoItem>> GetByListIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return Entities
			.Where(item => item.TodoList.Id.Equals(id)).Include(p => p.TodoList)
			.Include(p => p.TodoList)
			.ToListAsync(cancellationToken);
	}
}
