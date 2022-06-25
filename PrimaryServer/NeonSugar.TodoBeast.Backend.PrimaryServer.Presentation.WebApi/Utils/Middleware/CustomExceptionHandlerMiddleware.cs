using System;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Middleware.Base;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Middleware;
internal sealed class CustomExceptionHandlerMiddleware : BaseCustomMiddleware 
{
	public CustomExceptionHandlerMiddleware(RequestDelegate next) : base(next)
	{
		// empty
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await Next(context);
		}
		catch(Exception e)
		{
			await HandleExceptionAsync(context, e);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		var (code, body) = exception switch {
			ValidationException     e => (HttpStatusCode.BadRequest,          JsonSerializer.Serialize(new { Errors = e.Errors })),
			EntityNotFoundException e => (HttpStatusCode.NotFound,            JsonSerializer.Serialize(new { Error  = e.Message })),
			_                         => (HttpStatusCode.InternalServerError, JsonSerializer.Serialize(new { Error  = exception.Message }))
		};

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)code;
		return context.Response.WriteAsync(body);
	}
}
