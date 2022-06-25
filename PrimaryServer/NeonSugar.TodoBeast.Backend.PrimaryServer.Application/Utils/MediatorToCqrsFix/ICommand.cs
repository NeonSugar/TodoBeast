using MediatR;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
public interface ICommand<out TCommandAnswer> : IRequest<TCommandAnswer> 
where TCommandAnswer : ICommandAnswer 
{
	// empty
}

public interface ICommandAnswer : IResponse 
{
	// empty
}

internal interface ICommandHandler<in TCommand, TCommandAnswer> : IRequestHandler<TCommand, TCommandAnswer> 
where TCommand : ICommand<TCommandAnswer> 
where TCommandAnswer : ICommandAnswer 
{
	// empty
}