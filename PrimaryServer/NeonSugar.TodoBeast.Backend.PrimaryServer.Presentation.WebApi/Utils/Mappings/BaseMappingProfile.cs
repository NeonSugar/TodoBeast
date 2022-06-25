using AutoMapper;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings;
internal interface IMappingProfile<TSource, TDestination>
where TSource : class where TDestination : class 
{
	void Map(Profile profile);
	IMappingExpression<TSource, TDestination> MapTypes(Profile profile);
	IMappingExpression<TSource, TDestination> MapMembers(IMappingExpression<TSource, TDestination> expression);
}

internal abstract class BaseMappingProfile<TSource, TDestination> : IMappingProfile<TSource, TDestination>
where TSource : class where TDestination : class 
{
	public void Map(Profile profile)
		=> MapMembers(expression: MapTypes(profile: profile));

	public IMappingExpression<TSource, TDestination> MapTypes(Profile profile)
		=> profile.CreateMap<TSource, TDestination>();

	public abstract IMappingExpression<TSource, TDestination>
		MapMembers( IMappingExpression<TSource, TDestination> expression);
}
