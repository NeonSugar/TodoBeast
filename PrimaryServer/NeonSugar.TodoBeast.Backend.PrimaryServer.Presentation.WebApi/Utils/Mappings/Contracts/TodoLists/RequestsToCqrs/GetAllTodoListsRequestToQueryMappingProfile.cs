using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Queries;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.RequestsToCqrs;
internal sealed class GetAllTodoListsRequestToQueryMappingProfile : BaseMappingProfile<GetAllTodoListsRequest, GetAllTodoListsQuery> 
{
	public override IMappingExpression<GetAllTodoListsRequest, GetAllTodoListsQuery>
		MapMembers( IMappingExpression<GetAllTodoListsRequest, GetAllTodoListsQuery> expression) => expression;
	//  .ForMember(...);
}
