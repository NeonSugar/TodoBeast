using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.RequestsToCqrs;
internal sealed class UpdateTodoItemRequestToCommandMappingProfile : BaseMappingProfile<UpdateTodoListRequest, UpdateTodoListCommand> 
{
	public override IMappingExpression<UpdateTodoListRequest, UpdateTodoListCommand>
		MapMembers( IMappingExpression<UpdateTodoListRequest, UpdateTodoListCommand> expression) => expression
		.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
		.ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name));
	//  .ForMember(...);
}
