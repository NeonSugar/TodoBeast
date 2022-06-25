using System;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;
public sealed class EntityNotFoundException : ApplicationException 
{
	public EntityNotFoundException(string name, object key) 
		: base(message: $"Entity \"{name}\" with specified \"{key}\" was not found!") 
	{
		// empty
	}
}