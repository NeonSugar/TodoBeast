using AutoMapper;
using FluentValidation;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Extensions;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Mappings;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Commands;

public sealed record UpdateTodoItemCommand
: ICommand<UpdateTodoItemCommandAnswer>
{
	public Guid         Id { get; init; } = Guid.Empty;
	public string     Name { get; init; } = string.Empty;
	public string? Details { get; init; } = null;
	public Guid TodoListId { get; init; } = Guid.Empty;
	public Guid     UserId { get; init; } = Guid.Empty;
}

public sealed class UpdateTodoItemCommandAnswer
: ICommandAnswer, IMapWith<TodoItem>
{
	public void Map(Profile profile)
	{
		profile.CreateMap<TodoItem, UpdateTodoItemCommandAnswer>();
	}
}

internal sealed class UpdateTodoItemCommandHandler
: ICommandHandler<UpdateTodoItemCommand, UpdateTodoItemCommandAnswer>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public UpdateTodoItemCommandHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper
	)
	{
		this._unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<UpdateTodoItemCommandAnswer> Handle
		(UpdateTodoItemCommand command, CancellationToken cancellationToken)
	{
		var targetItems = await _unitOfWork.TodoItemRepository
			.GetByIdAsync(command.Id, cancellationToken);

		if (targetItems is null)
		{
			throw new EntityNotFoundException(name: nameof(targetItems), key: nameof(TodoList.Id));
		}
		if (targetItems.Any() is false)
		{
			throw new EntityNotFoundException(name: nameof(targetItems), key: nameof(TodoList.Id));
		}
		if (targetItems.Multiple() is true)
		{
			throw new MultipleEntitiesFoundException(name: nameof(targetItems), key: nameof(TodoList.Id));
		}

		var targetItem = targetItems.First();
		if (targetItem.TodoList.UserId.Equals(command.UserId) is false)
		{
			throw new CorruptedEntityDataException(name: nameof(targetItem), key: nameof(targetItem.TodoListId));
		}

		if (command.Name is not null) targetItem.Name = command.Name;
		if (command.Details is not null) targetItem.Details = command.Details;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return _mapper.Map<UpdateTodoItemCommandAnswer>(targetItem);
	}
}

internal sealed class UpdateTodoItemCommandValidator
: AbstractValidator<UpdateTodoItemCommand>
{
	public UpdateTodoItemCommandValidator()
	{
		RuleFor(list => list.Id).NotEmpty();
		RuleFor(list => list.Name).NotEmpty().MaximumLength(64);
		RuleFor(list => list.Details).NotEmpty().MaximumLength(140);
		RuleFor(list => list.TodoListId).NotEmpty();
	}
}
