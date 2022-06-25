using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.Contexts;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.UowRepositories.Repositories.Base;
internal abstract class BaseRepository<TEntity>
	: IBaseRepository<TEntity> where TEntity : class 
{
	private readonly AppDbContext _context;
	private readonly DbSet<TEntity> _dbEntities;

	internal BaseRepository(AppDbContext context) 
	{
		this._context = context;
		this._dbEntities = context.Set<TEntity>();
	}

	private protected DbSet<TEntity> Entities => _dbEntities;

	public Task AddAsync(TEntity entity, CancellationToken cancellationToken)
		=> _dbEntities.AddAsync(entity, cancellationToken).AsTask();

	public void Update(TEntity entity)
		=> _dbEntities.Update(entity);

	public void Remove(TEntity entity)
		=> _dbEntities.Remove(entity);
}
