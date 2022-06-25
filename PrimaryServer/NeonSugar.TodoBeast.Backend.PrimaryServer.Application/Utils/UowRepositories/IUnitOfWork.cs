using System;
using System.Threading;
using System.Threading.Tasks;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
public interface IUnitOfWork : IDisposable 
{
	ITodoListRepository TodoListRepository { get; }
	ITodoItemRepository TodoItemRepository { get; }
	// I...Repository ...Repository { get; }

	Task SaveChangesAsync(CancellationToken cancellationToken);
}