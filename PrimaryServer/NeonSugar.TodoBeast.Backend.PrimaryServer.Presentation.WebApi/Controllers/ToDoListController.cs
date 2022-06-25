using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Commands;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoLists.Queries;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Controllers.Base;
using NeonSugar.TodoBeast.Shared.Contracts.TodoLists;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Controllers;
public sealed class ToDoListController : BaseController 
{
	public ToDoListController(
		IMediator mediator,
		IMapper mapper
	) : base(mediator, mapper) 
	{
		// empty
	}

	[HttpPost]
	public async Task<ActionResult<GetTodoListResponse>>
		Get([FromBody] GetTodoListRequest request) 
	{
		return Ok(value: Mapper.Map<GetTodoListResponse>(source:
			await Mediator.SendQuery(Mapper.Map<GetTodoListQuery>(request)
				with { UserId = base.UserId } ) ));
	}

	[HttpPost]
	public async Task<ActionResult<GetAllTodoListsResponse>>
		GetAll([FromBody] GetAllTodoListsRequest request) 
	{
		return Ok(value: Mapper.Map<GetAllTodoListsResponse>(source:
			await Mediator.SendQuery(Mapper.Map<GetAllTodoListsQuery>(request)
				with { UserId = base.UserId } ) ));
	}

	[HttpPost]
	public async Task<ActionResult<CreateTodoListResponse>>
		Create([FromBody] CreateTodoListRequest request) 
	{
		return Ok(value: Mapper.Map<CreateTodoListResponse>(source:
			await Mediator.SendCommand(Mapper.Map<CreateTodoListCommand>(request)
				with { UserId = base.UserId } ) ));
	}

	[HttpPut]
	public async Task<ActionResult<UpdateTodoListResponse>>
		Update([FromBody] UpdateTodoListRequest request) 
	{
		return Ok(value: Mapper.Map<UpdateTodoListResponse>(source:
			await Mediator.SendCommand(Mapper.Map<UpdateTodoListCommand>(request)
				with { UserId = base.UserId } ) ));
	}

	[HttpDelete]
	public async Task<ActionResult<DeleteTodoListResponse>>
		Delete([FromBody] DeleteTodoListRequest request) 
	{
		return Ok(value: Mapper.Map<DeleteTodoListResponse>(source:
			await Mediator.SendCommand(Mapper.Map<DeleteTodoListCommand>(request)
				with { UserId = base.UserId } ) ));
	}
}