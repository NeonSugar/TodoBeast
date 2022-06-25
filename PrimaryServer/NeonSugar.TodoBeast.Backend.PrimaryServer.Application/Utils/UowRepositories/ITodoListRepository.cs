using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
public interface ITodoListRepository : IBaseRepository<TodoList> 
{
	Task<List<TodoList>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<List<TodoList>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

	Task<Guid> GetUserIdByIdAsync(Guid id, CancellationToken cancellationToken);
}