using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

public sealed class UpdateTodoItemRequest : BaseRequest 
{
	public Guid         Id { get; init; } = Guid.Empty;
	public string     Name { get; init; } = string.Empty;
	public string? Details { get; init; } = null;
	public Guid TodoListId { get; init; } = Guid.Empty;
}

public sealed class UpdateTodoItemResponse : BaseResponse 
{
	// empty
}
