using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoItems.RequestsToCqrs
{
	internal class DeleteTodoItemRequestToCommandMappingProfile : BaseMappingProfile<DeleteTodoItemRequest, DeleteTodoItemCommand>
	{
		public override IMappingExpression<DeleteTodoItemRequest, DeleteTodoItemCommand>
			MapMembers(IMappingExpression<DeleteTodoItemRequest, DeleteTodoItemCommand> expression) => expression
			.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));
	}
}
