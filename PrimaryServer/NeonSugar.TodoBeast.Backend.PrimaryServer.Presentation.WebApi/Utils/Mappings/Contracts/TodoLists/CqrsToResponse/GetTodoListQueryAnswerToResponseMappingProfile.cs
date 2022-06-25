using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Queries;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.CqrsToResponse;
internal sealed class GetTodoListQueryAnswerToResponseMappingProfile : BaseMappingProfile<GetTodoListQueryAnswer, GetTodoListResponse> 
{
	public override IMappingExpression<GetTodoListQueryAnswer, GetTodoListResponse>
		MapMembers( IMappingExpression<GetTodoListQueryAnswer, GetTodoListResponse> expression) => expression
		.ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name));
	//  .ForMember(...);
}
