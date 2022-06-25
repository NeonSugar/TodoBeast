using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
public interface ITodoItemRepository : IBaseRepository<TodoItem> 
{
	Task<List<TodoItem>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<List<TodoItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
	Task<List<TodoItem>> GetByListIdAsync(Guid listId, CancellationToken cancellationToken);
}