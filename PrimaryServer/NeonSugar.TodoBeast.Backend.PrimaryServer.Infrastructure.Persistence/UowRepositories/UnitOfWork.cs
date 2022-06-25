using System;
using System.Threading;
using System.Threading.Tasks;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.Contexts;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.UowRepositories.Repositories;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.UowRepositories;
internal sealed class UnitOfWork : IUnitOfWork, IDisposable 
{
	private readonly AppDbContext _context;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ITodoItemRepository _todoItemRepository;
	// private readonly I...Repository _...Repository;

	public UnitOfWork(AppDbContext context) 
	{
		this._context = context;
		this._todoListRepository = new TodoListRepository(context);
		this._todoItemRepository = new TodoItemRepository(context);
		// this._...Repository = new ...Repository(context);
	}

	public ITodoListRepository TodoListRepository => _todoListRepository;
	public ITodoItemRepository TodoItemRepository => _todoItemRepository;

	public async Task SaveChangesAsync(CancellationToken cancellationToken) 
	{
		await _context.SaveChangesAsync(cancellationToken);
	}
	public void Dispose() 
	{
		_context.Dispose();
	}
}