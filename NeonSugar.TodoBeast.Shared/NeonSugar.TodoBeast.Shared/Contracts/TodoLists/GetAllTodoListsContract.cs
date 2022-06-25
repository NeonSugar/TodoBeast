using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoLists;

public sealed class GetAllTodoListsRequest : BaseRequest 
{
	// empty
}

public sealed class GetAllTodoListsResponse : BaseResponse 
{
	public List<Item> TodoLists { get; init; } = new ();
	public sealed class Item 
	{
		public string Name { get; init; } = string.Empty;
	}
}
