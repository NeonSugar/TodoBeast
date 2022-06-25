using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.RequestsToCqrs;
internal sealed class DeleteTodoListRequestToCommandMappingProfile : BaseMappingProfile<DeleteTodoListRequest, DeleteTodoListCommand> 
{
	public override IMappingExpression<DeleteTodoListRequest, DeleteTodoListCommand>
		MapMembers( IMappingExpression<DeleteTodoListRequest, DeleteTodoListCommand> expression) => expression
		.ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));
	//  .ForMember(...);
}
