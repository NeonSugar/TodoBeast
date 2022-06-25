using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NeonSugar.TodoBeast.Backend.IdentityServer;
using NeonSugar.TodoBeast.Backend.IdentityServer.Contexts;
using NeonSugar.TodoBeast.Backend.IdentityServer.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services
	.AddIdentity<IdentityUser, IdentityRole>(config =>
	{
		config.Password.RequireUppercase = true;
		config.Password.RequireLowercase = true;

		config.Password.RequiredLength = 8;
	})
	.AddEntityFrameworkStores<AuthDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
	.AddAspNetIdentity<IdentityUser>()
	.AddInMemoryApiResources(Configuration.ApiResources)
	.AddInMemoryIdentityResources(Configuration.IdentityResources)
	.AddInMemoryApiScopes(Configuration.ApiScopes)
	.AddInMemoryClients(Configuration.Clients)
	.AddDeveloperSigningCredential();

builder.Services.ConfigureApplicationCookie(config =>
{
	config.Cookie.Name = "TodoBeast.Identity.Cookie";
	config.LoginPath   = "/Auth/Login";
	config.LogoutPath  = "/Auth/Logout";
});

//builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddMvcCore();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var serviceProvider = scope.ServiceProvider;
	try
	{
		var context = serviceProvider.GetRequiredService<AuthDbContext>();
		context.Database.EnsureCreated();
	}
	catch (Exception ex)
	{
		var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An exception occurred while application initialization");
	}
}

app.UseIdentityServer();
app.MapDefaultControllerRoute();

app.Run();
