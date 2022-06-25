using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoLists;

public sealed class DeleteTodoListRequest : BaseRequest 
{
	public Guid Id { get; init; } = Guid.Empty;
}

public sealed class DeleteTodoListResponse : BaseResponse 
{
	// empty
}
