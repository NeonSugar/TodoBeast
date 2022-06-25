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

public record DeleteTodoItemCommand : ICommand<DeleteTodoItemCommandAnswer>
{
	public Guid     Id { get; set; } = Guid.Empty;
	public Guid UserId { get; set; } = Guid.Empty;
}

public class DeleteTodoItemCommandAnswer
: ICommandAnswer, IMapWith<TodoItem>
{
	
	public void Map(Profile profile)
	{
		profile.CreateMap<TodoItem, DeleteTodoItemCommandAnswer>();
	}
}

internal sealed class DeleteTodoItemCommandHandler
: ICommandHandler<DeleteTodoItemCommand, DeleteTodoItemCommandAnswer>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public DeleteTodoItemCommandHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper
	)
	{
		this._unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<DeleteTodoItemCommandAnswer> Handle(DeleteTodoItemCommand command, CancellationToken cancellationToken)
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

		_unitOfWork.TodoItemRepository.Remove(targetItem);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return _mapper.Map<DeleteTodoItemCommandAnswer>(targetItem);
	}
}

internal sealed class DeleteTodoItemCommandValidator
: AbstractValidator<DeleteTodoItemCommand>
{
	public DeleteTodoItemCommandValidator()
	{
		RuleFor(item => item.Id).NotEmpty();
		RuleFor(item => item.UserId).NotEmpty();
	}
}