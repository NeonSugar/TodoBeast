using AutoMapper;
using FluentValidation;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;
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

public sealed record CreateTodoItemCommand : ICommand<CreateTodoItemCommandAnswer>
{
	public string Name { get; set; } = string.Empty;
	public string? Details { get; set; } = null;
	public Guid TodoListId { get; set; } = Guid.Empty;
	public Guid     UserId { get; set; } = Guid.Empty;
}

public sealed class CreateTodoItemCommandAnswer
: ICommandAnswer, IMapWith<TodoItem>
{
	public Guid Id { get; set; }

	public void Map(Profile profile)
	{
		profile.CreateMap<TodoItem, CreateTodoItemCommandAnswer>()
			.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));
	}
}

internal sealed class CreateTodoItemCommandHandler
: ICommandHandler<CreateTodoItemCommand, CreateTodoItemCommandAnswer>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateTodoItemCommandHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper
	)
	{
		this._unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<CreateTodoItemCommandAnswer> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
	{
		var item = new TodoItem
		{
			TodoListId = request.TodoListId,
			Name = request.Name,
			Details = request.Details
		};
		if (request.UserId.Equals(await _unitOfWork.TodoListRepository.GetUserIdByIdAsync(request.TodoListId ,cancellationToken)) is false)
		{
			throw new CorruptedEntityDataException(name: nameof(item), key: nameof(item.TodoList.UserId));
		}
		await _unitOfWork.TodoItemRepository.AddAsync(item, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return _mapper.Map<CreateTodoItemCommandAnswer>(item);
	}
}

internal sealed class CreateTodoItemCommandValidator
: AbstractValidator<CreateTodoItemCommand>
{
	public CreateTodoItemCommandValidator()
	{
		RuleFor(list => list.Name).NotEmpty().MaximumLength(64);
		RuleFor(list => list.TodoListId).NotEmpty();
		RuleFor(list => list.UserId).NotEmpty();
	}
}
