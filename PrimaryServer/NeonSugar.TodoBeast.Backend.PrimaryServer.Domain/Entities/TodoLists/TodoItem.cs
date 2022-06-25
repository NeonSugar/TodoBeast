using System;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;
public sealed class TodoItem 
{
	public Guid           Id { get; set; } = Guid.NewGuid();
	public string       Name { get; set; } = string.Empty;
	public string?   Details { get; set; } = null!;

	public Guid   TodoListId { get; set; } = Guid.Empty;
	public TodoList TodoList { get; set; } = null!;
}