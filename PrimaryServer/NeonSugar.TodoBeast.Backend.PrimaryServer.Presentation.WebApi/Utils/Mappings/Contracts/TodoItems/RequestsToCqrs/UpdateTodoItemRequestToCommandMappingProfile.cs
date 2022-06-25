using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoItems.RequestsToCqrs;
internal sealed class UpdateTodoItemRequestToCommandMappingProfile : BaseMappingProfile<UpdateTodoItemRequest, UpdateTodoItemCommand> 
{
	public override IMappingExpression<UpdateTodoItemRequest, UpdateTodoItemCommand>
		MapMembers( IMappingExpression<UpdateTodoItemRequest, UpdateTodoItemCommand> expression) => expression
		.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
		.ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name))
		.ForMember(destination => destination.Details, options => options.MapFrom(source => source.Details))
		.ForMember(destination => destination.TodoListId, options => options.MapFrom(source => source.TodoListId));


	//  .ForMember(...);
}
