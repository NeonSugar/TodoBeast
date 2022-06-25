using AutoMapper;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Queries;
using NeonSugar.TodoBeast.Shared.Contracts.TodoItems;
using System.Linq;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.InterfaceAdapters.Presentation.WebApi.Utils.Mappings.Contracts.TodoItems.CqrsToResponse
{
	internal class GetAllTodoItemsQueryAnswerToResponseMappingProfile : BaseMappingProfile<GetAllTodoItemsQueryAnswer, GetAllTodoItemsResponse>
	{
		public override IMappingExpression<GetAllTodoItemsQueryAnswer, GetAllTodoItemsResponse>
			MapMembers(IMappingExpression<GetAllTodoItemsQueryAnswer, GetAllTodoItemsResponse> expression) => expression

			.ForMember(destination => destination.TodoItems, options => options.MapFrom(
				source => source.TodoItems.Select(
					item => new GetAllTodoItemsResponse.Item()
					{
						Name = item.Name,
						Details = item.Details!
					}
				)
			));
	}
}
