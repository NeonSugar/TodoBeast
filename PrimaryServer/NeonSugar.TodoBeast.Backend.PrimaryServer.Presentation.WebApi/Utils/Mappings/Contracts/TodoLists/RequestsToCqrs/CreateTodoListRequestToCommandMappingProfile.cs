using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoLists.RequestsToCqrs;
internal sealed class CreateTodoListRequestToCommandMappingProfile : BaseMappingProfile<CreateTodoListRequest, CreateTodoListCommand> 
{
	public override IMappingExpression<CreateTodoListRequest, CreateTodoListCommand>
		MapMembers( IMappingExpression<CreateTodoListRequest, CreateTodoListCommand> expression) => expression
		.ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name));
	//  .ForMember(...);
}
