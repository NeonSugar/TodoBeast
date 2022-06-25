using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Behaviors;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
where TRequest : IRequest<TResponse> 
{
	public async Task<TResponse> Handle(TRequest request,
		CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next
	) 
	{
		Log.Information(messageTemplate: "Application layer received the following request: {Request}", request);
		return await next();
	}
}
