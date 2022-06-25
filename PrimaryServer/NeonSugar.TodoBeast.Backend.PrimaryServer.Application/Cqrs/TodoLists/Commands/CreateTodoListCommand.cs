using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Mappings;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;


// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MissingIndent


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;

public sealed record CreateTodoListCommand
: ICommand<CreateTodoListCommandAnswer> 
{
	public Guid   UserId { get; init; } = Guid.Empty;
	public string Name   { get; init; } = string.Empty;
}

public sealed class CreateTodoListCommandAnswer
: ICommandAnswer, IMapWith<TodoList> 
{
	public Guid Id { get; init; } = Guid.Empty;
	public void Map(Profile profile) 
	{
		profile.CreateMap<TodoList, CreateTodoListCommandAnswer>()
			.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));
	}
}

internal sealed class CreateTodoListCommandHandler
: ICommandHandler<CreateTodoListCommand, CreateTodoListCommandAnswer> 
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateTodoListCommandHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper
	) 
	{
		this._unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<CreateTodoListCommandAnswer> Handle
		(CreateTodoListCommand command, CancellationToken cancellationToken) 
	{
		var newTodoList = new TodoList() 
		{
			UserId = command.UserId,
			Name   = command.Name
		};
		
		await _unitOfWork.TodoListRepository.AddAsync(newTodoList, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return _mapper.Map<CreateTodoListCommandAnswer>(newTodoList);
	}
}

internal sealed class CreateTodoListCommandValidator
: AbstractValidator<CreateTodoListCommand> 
{
	public CreateTodoListCommandValidator() 
	{
		RuleFor(list => list.UserId).NotEmpty();
		RuleFor(list => list.Name).NotEmpty().MaximumLength(64);
	}
}
