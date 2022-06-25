using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Mappings;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MissingIndent


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Queries;

public sealed record GetAllTodoListsQuery
: IQuery<GetAllTodoListsQueryAnswer> 
{
	public Guid UserId { get; init; } = Guid.Empty;
}

public sealed class GetAllTodoListsQueryAnswer
: IQueryAnswer, IMapWith<List<TodoList>> 
{
	public List<Item> TodoLists { get; init; } = new ();

	public void Map(Profile profile) 
	{
		profile.CreateMap<List<TodoList>, GetAllTodoListsQueryAnswer>()
			.ForMember(destination => destination.TodoLists, options => options.MapFrom(source => source.Select(list => new Item() { Name = list.Name }).ToList()));
	}
	public sealed class Item 
	{
		public string Name { get; init; } = string.Empty;
	}
}

internal sealed class GetAllTodoListsQueryHandler
: IQueryHandler<GetAllTodoListsQuery, GetAllTodoListsQueryAnswer> 
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllTodoListsQueryHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper
	) 
	{
		this._unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<GetAllTodoListsQueryAnswer> Handle
		(GetAllTodoListsQuery query, CancellationToken cancellationToken) 
	{
		var targetLists = await _unitOfWork.TodoListRepository
			.GetByUserIdAsync(query.UserId, cancellationToken);

		if( targetLists is null) 
		{
			throw new EntityNotFoundException(name: nameof(targetLists), key: nameof(TodoList.UserId));
		}
		if( targetLists.Any() is false) 
		{
			throw new EntityNotFoundException(name: nameof(targetLists), key: nameof(TodoList.UserId));
		}

		return _mapper.Map<GetAllTodoListsQueryAnswer>(targetLists);
	}
}

internal sealed class GetAllToDoListsQueryValidator
: AbstractValidator<GetAllTodoListsQuery> 
{
	public GetAllToDoListsQueryValidator() 
	{
		RuleFor(list => list.UserId).NotEmpty();
	}
}