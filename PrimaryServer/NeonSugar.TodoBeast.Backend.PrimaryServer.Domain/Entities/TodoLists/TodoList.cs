using System;
using System.Collections.Generic;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;
public sealed class TodoList 
{
	public Guid   Id     { get; init; } = Guid.NewGuid();
	public Guid   UserId { get; init; } = Guid.Empty;
	public string Name   { get; set;  } = string.Empty;

	public List<TodoItem> Items { get; init; } = new ();
}
