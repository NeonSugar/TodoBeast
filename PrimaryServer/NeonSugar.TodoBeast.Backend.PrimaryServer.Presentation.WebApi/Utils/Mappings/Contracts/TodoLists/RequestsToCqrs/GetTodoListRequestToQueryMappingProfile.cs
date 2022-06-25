using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Queries;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.RequestsToCqrs;
internal sealed class GetTodoListRequestToQueryMappingProfile : BaseMappingProfile<GetTodoListRequest, GetTodoListQuery> 
{
	public override IMappingExpression<GetTodoListRequest, GetTodoListQuery>
		MapMembers( IMappingExpression<GetTodoListRequest, GetTodoListQuery> expression) => expression
		.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));
	//  .ForMember(...);
}
