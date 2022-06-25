using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoLists;

public sealed class UpdateTodoListRequest : BaseRequest 
{
	public Guid    Id     { get; init; } = Guid.Empty;
	public string? Name   { get; init; } = null;
}

public sealed class UpdateTodoListResponse : BaseResponse 
{
	// empty
}
