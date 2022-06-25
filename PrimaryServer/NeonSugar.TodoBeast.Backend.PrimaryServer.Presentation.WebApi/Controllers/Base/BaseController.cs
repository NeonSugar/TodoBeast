using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Controllers.Base;

[ApiController]
[Route(template: "/api/[controller]/[action]")]
[Authorize]
public abstract class BaseController : ControllerBase 
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	private protected BaseController(
		IMediator mediator,
		IMapper mapper
	) 
	{
		this._mediator = mediator;
		this._mapper = mapper;
	}

	private protected IMediator Mediator => _mediator;
	private protected IMapper   Mapper   => _mapper;

	private protected Guid UserId 
	{
		get 
		{
			// We should get rid of UserId in ICurrentUserService
			// as well as from entire ICurrentUserService

			// We need to have User here
			// because User can have his unique claims
			// which can be helpful
			if(User.Identity?.IsAuthenticated is true) 
			{
				var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var parsed = Guid.TryParse(idClaim, out var guid);

				return parsed is true ? guid : throw new ApplicationException(message:
					$"{nameof(User)}'s {nameof(ClaimTypes.NameIdentifier)} hasn't been found."
				);
			}
			return Guid.Empty;
		}
	}
}