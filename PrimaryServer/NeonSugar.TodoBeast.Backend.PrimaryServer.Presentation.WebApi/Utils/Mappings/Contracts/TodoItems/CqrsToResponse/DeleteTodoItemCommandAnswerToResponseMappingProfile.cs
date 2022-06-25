using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoItems.CqrsToResponse
{
	internal class DeleteTodoItemCommandAnswerToResponseMappingProfile : BaseMappingProfile<DeleteTodoItemCommandAnswer, DeleteTodoItemResponse>
	{
		public override IMappingExpression<DeleteTodoItemCommandAnswer, DeleteTodoItemResponse>
			MapMembers( IMappingExpression<DeleteTodoItemCommandAnswer, DeleteTodoItemResponse> expression) => expression;
	}
}
