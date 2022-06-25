using AutoMapper;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Mappings;
internal interface IMapWith<TSourceType> 
where TSourceType : notnull 
{
	void Map(Profile profile);
}