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
internal sealed class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository 
{
	public TodoListRepository(AppDbContext context) : base(context) 
	{
		// empty
	}

	public Task<List<TodoList>> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
	{
		return Entities
			.Where(list => list.Id.Equals(id))
			.Include(p => p.Items)
			.ToListAsync(cancellationToken);
	}
	public Task<List<TodoList>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken) 
	{
		return Entities
			.Where(list => list.UserId.Equals(userId))
			.Include(p => p.Items)
			.ToListAsync(cancellationToken);
	}

	public Task<Guid> GetUserIdByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return Entities
			.Where(list => list.Id.Equals(id))
			.Select(list => list.UserId)
			.SingleAsync();
	}
}
