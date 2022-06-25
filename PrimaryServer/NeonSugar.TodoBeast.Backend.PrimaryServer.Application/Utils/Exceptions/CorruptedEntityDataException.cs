using System;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;
internal sealed class CorruptedEntityDataException : ApplicationException 
{
	internal CorruptedEntityDataException(string name, object key) 
		: base(message: $"Entity \"{name}\" has corrupted \"{key}\"!") 
	{
		// empty
	}
}
