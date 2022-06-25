using System;
using System.Linq;
using System.Reflection;
using AutoMapper;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings;
internal sealed  class AssemblyMappingProfile : Profile 
{
	public AssemblyMappingProfile() => Apply();
	private void Apply() 
	{
		var targetTypes = Assembly.GetExecutingAssembly().GetTypes()
			.Where(type => type.IsAbstract is false && type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMappingProfile<,>)))
			.ToList();

		foreach(var type in targetTypes)
		{
			type.GetMethod(nameof(IMappingProfile<object, object>.Map))
				?.Invoke(Activator.CreateInstance(type), new object[] { this });
		}
	}
}
