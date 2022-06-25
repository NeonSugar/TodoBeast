using System;
using System.Collections;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Extensions;
internal static class CollectionExtensions 
{
	internal static bool Multiple(this ICollection collection) 
	{
		ArgumentNullException.ThrowIfNull(collection);
		return collection?.Count > 1;
	}
}
