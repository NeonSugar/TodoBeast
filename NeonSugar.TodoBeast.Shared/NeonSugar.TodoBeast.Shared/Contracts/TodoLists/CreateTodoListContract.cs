using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoLists;

public sealed class CreateTodoListRequest : BaseRequest 
{
	public string Name { get; init; } = string.Empty;
}

public sealed class CreateTodoListResponse : BaseResponse 
{
	public Guid Id { get; init; } = Guid.Empty;
}
