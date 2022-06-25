using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.CqrsToResponse;
internal sealed class DeleteTodoListCommandAnswerToResponseMappingProfile : BaseMappingProfile<DeleteTodoListCommandAnswer, DeleteTodoListResponse> 
{
	public override IMappingExpression<DeleteTodoListCommandAnswer, DeleteTodoListResponse>
		MapMembers( IMappingExpression<DeleteTodoListCommandAnswer, DeleteTodoListResponse> expression) => expression;
	//  .ForMember(...);
}
