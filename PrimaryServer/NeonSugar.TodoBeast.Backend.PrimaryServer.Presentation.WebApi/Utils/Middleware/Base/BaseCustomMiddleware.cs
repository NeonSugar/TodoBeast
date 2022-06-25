using Microsoft.AspNetCore.Http;


namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Middleware.Base;
internal abstract class BaseCustomMiddleware 
{
    private readonly RequestDelegate _next;
    internal BaseCustomMiddleware(RequestDelegate next) 
    {
        this._next = next;
    }

    private protected RequestDelegate Next => _next;
}