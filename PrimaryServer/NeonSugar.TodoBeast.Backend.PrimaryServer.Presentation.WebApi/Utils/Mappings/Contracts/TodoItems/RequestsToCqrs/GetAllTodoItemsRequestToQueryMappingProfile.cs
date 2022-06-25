using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Queries;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoItems.RequestsToCqrs
{
	internal class GetAllTodoItemsRequestToQueryMappingProfile : BaseMappingProfile<GetAllTodoItemsRequest, GetAllTodoItemsQuery>
	{
		public override IMappingExpression<GetAllTodoItemsRequest, GetAllTodoItemsQuery> 
			MapMembers( IMappingExpression<GetAllTodoItemsRequest, GetAllTodoItemsQuery> expression) => expression
			.ForMember(destination => destination.ListId, options => options.MapFrom(source => source.ListId));
	}
}
