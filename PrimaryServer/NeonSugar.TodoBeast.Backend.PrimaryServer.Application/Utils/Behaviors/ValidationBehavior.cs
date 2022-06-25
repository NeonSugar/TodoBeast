using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Behaviors;
internal sealed class ValidationBehavior<TRequest, TResponse> 
	: IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> 
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;
	public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) 
	{
		this._validators = validators;
	}

	public Task<TResponse> Handle(TRequest request,
		CancellationToken cancellationToken, 
		RequestHandlerDelegate<TResponse> next
	) 
	{
		var context =
			new ValidationContext<TRequest>(request);

		var failures = _validators
			.Select(validator => validator.Validate(context))
			.SelectMany(result => result.Errors).ToList();

		if(failures.Any()) 
		{
			throw new ValidationException(failures);
		}

		return next();
	}
}