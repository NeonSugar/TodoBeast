using System;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;
internal sealed class MultipleEntitiesFoundException : ApplicationException 
{
	internal MultipleEntitiesFoundException(string name, object key) 
		: base(message: $"Multiple entities \"{name}\" with specified \"{key}\" were found!") 
	{
		// empty
	}
}
