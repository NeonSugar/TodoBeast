using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoLists;

public sealed class GetTodoListRequest : BaseRequest 
{
	public Guid Id { get; init; } = Guid.Empty;
}

public sealed class GetTodoListResponse : BaseResponse 
{
	public string Name { get; init; } = string.Empty;
}
