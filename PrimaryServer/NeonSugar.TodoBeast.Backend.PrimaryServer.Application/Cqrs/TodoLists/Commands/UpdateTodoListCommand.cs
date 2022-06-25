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


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;

public sealed record UpdateTodoListCommand
: ICommand<UpdateTodoListCommandAnswer> 
{
	public Guid    Id     { get; init; } = Guid.Empty;
	public Guid    UserId { get; init; } = Guid.Empty;
	public string? Name   { get; init; } = null;
}

public sealed class UpdateTodoListCommandAnswer
: ICommandAnswer, IMapWith<TodoList> 
{
	public void Map(Profile profile)
	{
		profile.CreateMap<TodoList, UpdateTodoListCommandAnswer>();
	}
}

internal sealed class UpdateTodoListCommandHandler
: ICommandHandler<UpdateTodoListCommand, UpdateTodoListCommandAnswer> 
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public UpdateTodoListCommandHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper
	) 
	{
		this._unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<UpdateTodoListCommandAnswer> Handle
		(UpdateTodoListCommand command, CancellationToken cancellationToken) 
	{
		var targetLists = await _unitOfWork.TodoListRepository
			.GetByIdAsync(command.Id, cancellationToken);

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
		if( targetList.UserId.Equals(command.UserId) is false) 
		{
			throw new CorruptedEntityDataException(name: nameof(targetList), key: nameof(targetList.UserId));
		}

		if(command.Name is not null) targetList.Name = command.Name;
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return _mapper.Map<UpdateTodoListCommandAnswer>(targetList);
	}
}

internal sealed class UpdateToDoListCommandValidator
: AbstractValidator<UpdateTodoListCommand> 
{
	public UpdateToDoListCommandValidator() 
	{
		RuleFor(list => list.Id).NotEmpty();
		RuleFor(list => list.UserId).NotEmpty();
		RuleFor(list => list.Name).NotEmpty().MaximumLength(64);
	}
}