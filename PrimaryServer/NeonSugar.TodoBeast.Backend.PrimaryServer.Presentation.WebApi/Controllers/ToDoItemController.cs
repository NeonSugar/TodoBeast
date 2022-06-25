using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Queries;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Commands;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Controllers.Base;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;
using System.Threading.Tasks;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Controllers;

public class ToDoItemController : BaseController
{
	public ToDoItemController(
		IMediator mediator, 
		IMapper mapper
	) : base(mediator, mapper)
	{
		// empty
	}

	[HttpPost]
	public async Task<ActionResult<GetAllTodoItemsResponse>>
		GetAll([FromBody] GetAllTodoItemsRequest request)
	{
		return Ok(value: Mapper.Map<GetAllTodoItemsResponse>(source:
			await Mediator.SendQuery(Mapper.Map<GetAllTodoItemsQuery>(request)
				with { UserId = base.UserId })));
	}

	[HttpPost]
	public async Task<ActionResult<CreateTodoItemResponse>>
		Create([FromBody] CreateTodoItemRequest request)
	{
		return Ok(value: Mapper.Map<CreateTodoItemResponse>(source:
			await Mediator.SendCommand(Mapper.Map<CreateTodoItemCommand>(request)
				with { UserId = base.UserId })));
	}

	[HttpPut]
	public async Task<ActionResult<UpdateTodoItemResponse>>
		Update([FromBody] UpdateTodoItemRequest request)
	{
		return Ok(value: Mapper.Map<UpdateTodoItemResponse>(source:
			await Mediator.SendCommand(Mapper.Map<UpdateTodoItemCommand>(request)
				with { UserId = base.UserId })));
	}

	[HttpDelete]
	public async Task<ActionResult<DeleteTodoItemResponse>>
		Delete([FromBody] DeleteTodoItemRequest request)
	{
		return Ok(value: Mapper.Map<DeleteTodoItemResponse>(source:
			await Mediator.SendCommand(Mapper.Map<DeleteTodoItemCommand>(request)
				with { UserId = base.UserId })));
	}
}
