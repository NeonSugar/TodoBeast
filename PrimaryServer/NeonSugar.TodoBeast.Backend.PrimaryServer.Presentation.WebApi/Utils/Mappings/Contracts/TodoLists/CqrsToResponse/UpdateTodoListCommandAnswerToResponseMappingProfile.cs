using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.CqrsToResponse;
internal sealed class UpdateTodoListCommandAnswerToResponseMappingProfile : BaseMappingProfile<UpdateTodoListCommandAnswer, UpdateTodoListResponse> 
{
	public override IMappingExpression<UpdateTodoListCommandAnswer, UpdateTodoListResponse>
		MapMembers( IMappingExpression<UpdateTodoListCommandAnswer, UpdateTodoListResponse> expression) => expression;
	//  .ForMember(...);
}
