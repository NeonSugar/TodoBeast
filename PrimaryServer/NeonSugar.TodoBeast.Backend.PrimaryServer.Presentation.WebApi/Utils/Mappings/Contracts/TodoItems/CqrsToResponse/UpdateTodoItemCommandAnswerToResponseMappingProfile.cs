using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoItems.CqrsToResponse
{
	internal class UpdateTodoItemCommandAnswerToResponseMappingProfile : BaseMappingProfile<UpdateTodoItemCommandAnswer, UpdateTodoItemResponse>
	{
		public override IMappingExpression<UpdateTodoItemCommandAnswer, UpdateTodoItemResponse>
			MapMembers( IMappingExpression<UpdateTodoItemCommandAnswer, UpdateTodoItemResponse> expression) => expression;
	}
}
