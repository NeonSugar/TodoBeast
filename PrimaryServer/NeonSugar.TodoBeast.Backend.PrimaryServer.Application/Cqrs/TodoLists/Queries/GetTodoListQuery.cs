using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Extensions;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Mappings;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MissingIndent


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Queries;

public sealed record GetTodoListQuery
: IQuery<GetTodoListQueryAnswer> 
{
	public Guid Id     { get; init; } = Guid.Empty;
	public Guid UserId { get; init; } = Guid.Empty;
}

public sealed class GetTodoListQueryAnswer
: IQueryAnswer, IMapWith<TodoList> 
{
	public string Name { get; init; } = string.Empty;
	public void Map(Profile profile) 
	{
		profile.CreateMap<TodoList, GetTodoListQueryAnswer>()
			.ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name));
	}
}

internal sealed class GetTodoListQueryHandler
: IQueryHandler<GetTodoListQuery, GetTodoListQueryAnswer> 
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetTodoListQueryHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper
	) 
	{
		this._unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<GetTodoListQueryAnswer> Handle
		(GetTodoListQuery query, CancellationToken cancellationToken)  
	{
		var targetLists = await _unitOfWork.TodoListRepository
			.GetByIdAsync(query.Id, cancellationToken);

		if( targetLists is null) 
		{
			throw new EntityNotFoundException(name: nameof(targetLists), key: nameof(TodoList.Id));
		}
		if( targetLists.Any() is false) 
		{
			throw new EntityNotFoundException(name: nameof(targetLists), key: nameof(TodoList.Id));
		}
		if( targetLists.Multiple() is true) 
		{
			throw new MultipleEntitiesFoundException(name: nameof(targetLists), key: nameof(TodoList.Id));
		}

		var targetList = targetLists.First();
		if( targetList.UserId.Equals(query.UserId) is false) 
		{
			throw new CorruptedEntityDataException(name: nameof(targetList), key: nameof(targetList.UserId));
		}

		return _mapper.Map<GetTodoListQueryAnswer>(targetList);
	}
}

internal sealed class GetTodoListQueryValidator
: AbstractValidator<GetTodoListQuery>
{
	public GetTodoListQueryValidator() 
	{
		RuleFor(list => list.Id).NotEmpty();
		RuleFor(list => list.UserId).NotEmpty();
	}
}