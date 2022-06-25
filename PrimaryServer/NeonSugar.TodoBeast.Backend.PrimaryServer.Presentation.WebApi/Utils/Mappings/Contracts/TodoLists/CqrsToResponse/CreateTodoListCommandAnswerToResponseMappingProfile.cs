using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.CqrsToResponse;
internal sealed class CreateTodoListCommandAnswerToResponseMappingProfile : BaseMappingProfile<CreateTodoListCommandAnswer, CreateTodoListResponse> 
{
	public override IMappingExpression<CreateTodoListCommandAnswer, CreateTodoListResponse>
		MapMembers( IMappingExpression<CreateTodoListCommandAnswer, CreateTodoListResponse> expression) => expression
		.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));
	//  .ForMember(...);
}
