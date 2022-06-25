using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

public sealed class DeleteTodoItemRequest : BaseRequest 
{
	public Guid         Id { get; init; } = Guid.Empty;
	public Guid TodoListId { get; init; } = Guid.Empty;
}

public sealed class DeleteTodoItemResponse : BaseResponse 
{
	// empty
}
