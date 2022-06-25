using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoItems.CqrsToResponse
{
	internal class CreateTodoItemCommandAnswerToResponseMappingProfile : BaseMappingProfile<CreateTodoItemCommandAnswer, CreateTodoItemResponse>
	{
		public override IMappingExpression<CreateTodoItemCommandAnswer, CreateTodoItemResponse>
			MapMembers(IMappingExpression<CreateTodoItemCommandAnswer, CreateTodoItemResponse> expression) => expression
			.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));
	}
}
