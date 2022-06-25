using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Extensions;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Infrastructure.Persistence.Extensions;
using ApplicationAssemblyMarker = NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Mappings.AssemblyMappingMarker;
using Serilog;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi;
using Microsoft.Extensions.Options;

Console.InputEncoding
	= Console.OutputEncoding
		= Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((_, _, loggerConfiguration) => {
	loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddAuthentication(config =>
{
	config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
	options.Authority = "https://localhost:7155";
	options.Audience  = "TodoBeastWebApi";
	options.RequireHttpsMetadata = false;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddAutoMapper(config => {

	config.AddMaps(
		Assembly.GetExecutingAssembly(),
		Assembly.GetAssembly(typeof(ApplicationAssemblyMarker))
	);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration
	.GetConnectionString(name: "Default")
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen(options =>
{
	options.CustomSchemaIds(s => s.FullName.Replace("+", "."));
});
builder.Services.AddSwaggerGen();


var app = builder.Build();

if(app.Environment.IsDevelopment()) 
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();