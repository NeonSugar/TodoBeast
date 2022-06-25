using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoItems.RequestsToCqrs
{
	internal class CreateTodoItemRequestToCommandMappingProfile : BaseMappingProfile<CreateTodoItemRequest, CreateTodoItemCommand>
	{
		public override IMappingExpression<CreateTodoItemRequest, CreateTodoItemCommand>
			MapMembers(IMappingExpression<CreateTodoItemRequest, CreateTodoItemCommand> expression) => expression
			.ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name))
			.ForMember(destination => destination.TodoListId, options => options.MapFrom(source => source.TodoListId));

	}
}
