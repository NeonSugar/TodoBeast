using System.Linq;
using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Queries;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.CqrsToResponse;
internal sealed class GetAllTodoListsQueryAnswerToResponseMappingProfile : BaseMappingProfile<GetAllTodoListsQueryAnswer, GetAllTodoListsResponse> 
{
	public override IMappingExpression<GetAllTodoListsQueryAnswer, GetAllTodoListsResponse>
		MapMembers( IMappingExpression<GetAllTodoListsQueryAnswer, GetAllTodoListsResponse> expression) => expression

		.ForMember(destination => destination.TodoLists, options => options.MapFrom(
			source => source.TodoLists.Select(
				item => new GetAllTodoListsResponse.Item() { Name = item.Name }
			).ToList()
		));

	//  .ForMember(...);
}
