using System.Threading;
using System.Threading.Tasks;
using MediatR;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
public static class MediatorExtensions 
{
	public static Task<TQueryAnswer> SendQuery<TQueryAnswer>(
		this IMediator mediator, IQuery<TQueryAnswer> query,
		CancellationToken cancellationToken = default
	) where TQueryAnswer : IQueryAnswer 
	{
		return mediator.Send(query, cancellationToken);
	}

	public static Task<TCommandAnswer> SendCommand<TCommandAnswer>(
		this IMediator mediator, ICommand<TCommandAnswer> command,
		CancellationToken cancellationToken = default
	) where TCommandAnswer : ICommandAnswer 
	{
		return mediator.Send(command, cancellationToken);
	}
}
