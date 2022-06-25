using NeonSugar.TodoBeast.Shared.Contracts.Base;


namespace NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

public sealed class GetAllTodoItemsRequest : BaseRequest 
{
	public Guid ListId { get; set; }
}

public sealed class GetAllTodoItemsResponse : BaseResponse 
{
	public List<Item> TodoItems { get; init; } = new ();
	public sealed class Item 
	{
		public string    Name { get; init; } = string.Empty;
		public string Details { get; init; } = string.Empty;
	}
}
