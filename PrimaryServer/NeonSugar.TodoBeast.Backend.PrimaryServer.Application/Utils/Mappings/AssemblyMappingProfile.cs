using System;
using System.Linq;
using System.Reflection;
using AutoMapper;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Mappings;
internal sealed class AssemblyMappingProfile : Profile 
{
	public AssemblyMappingProfile() => Apply();
	private void Apply() 
	{
		var targetTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
			.Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
			.ToList();

		foreach(var type in targetTypes) 
		{
			type.GetMethod(nameof(IMapWith<object>.Map))
				?.Invoke(Activator.CreateInstance(type), new object[] { this });
		}
	}
}