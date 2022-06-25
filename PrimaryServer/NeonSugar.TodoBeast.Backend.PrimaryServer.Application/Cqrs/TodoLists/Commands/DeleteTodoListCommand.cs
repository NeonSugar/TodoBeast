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

public sealed record DeleteTodoListCommand
: ICommand<DeleteTodoListCommandAnswer> 
{
	public Guid Id     { get; init; } = Guid.Empty;
	public Guid UserId { get; init; } = Guid.Empty;
}

public sealed class DeleteTodoListCommandAnswer
: ICommandAnswer, IMapWith<TodoList> 
{
	public void Map(Profile profile) 
	{
		profile.CreateMap<TodoList, DeleteTodoListCommandAnswer>();
	}
}

internal sealed class DeleteTodoListCommandHandler
: ICommandHandler<DeleteTodoListCommand, DeleteTodoListCommandAnswer> 
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public DeleteTodoListCommandHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper
	) 
	{
		this._unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<DeleteTodoListCommandAnswer> Handle
		(DeleteTodoListCommand command,	CancellationToken cancellationToken) 
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

		_unitOfWork.TodoListRepository.Remove(targetList);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return _mapper.Map<DeleteTodoListCommandAnswer>(targetList);
	}
}

internal sealed class DeleteTodoListCommandValidator
: AbstractValidator<DeleteTodoListCommand> 
{
	public DeleteTodoListCommandValidator() 
	{
		RuleFor(list => list.Id).NotEmpty();
		RuleFor(list => list.UserId).NotEmpty();
	}
}