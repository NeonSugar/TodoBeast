using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

public sealed class CreateTodoItemRequest : BaseRequest 
{
	public string     Name { get; init; } = string.Empty;
	public Guid TodoListId { get; init; } = Guid.Empty;
}

public sealed class CreateTodoItemResponse : BaseResponse 
{
	public Guid Id { get; init; } = Guid.Empty;
}
