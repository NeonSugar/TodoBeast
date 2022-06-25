using System.Threading;
using System.Threading.Tasks;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
public interface IBaseRepository<in TEntity> where TEntity : class 
{
	Task AddAsync(TEntity entity,
		CancellationToken cancellationToken);

	void Update(TEntity entity);
	void Remove(TEntity entity);
}