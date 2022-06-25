using MediatR;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
public interface IQuery<out TQueryAnswer> : IRequest<TQueryAnswer> 
where TQueryAnswer : IQueryAnswer 
{
	// empty
}

public interface IQueryAnswer : IResponse 
{
	// empty
}

internal interface IQueryHandler<in TQuery, TQueryAnswer> : IRequestHandler<TQuery, TQueryAnswer>
where TQuery : IQuery<TQueryAnswer> 
where TQueryAnswer : IQueryAnswer 
{
	// empty
}