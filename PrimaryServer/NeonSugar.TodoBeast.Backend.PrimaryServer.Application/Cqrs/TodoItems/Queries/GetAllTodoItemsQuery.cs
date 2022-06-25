using AutoMapper;
using FluentValidation;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Exceptions;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.Mappings;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.MediatorToCqrsFix;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Utils.UowRepositories;
using NeonSugar.TodoBeast.Backend.PrimaryServer.Domain.Entities.TodoLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeonSugar.TodoBeast.Backend.PrimaryServer.Application.Cqrs.TodoItems.Queries
{
	public sealed record GetAllTodoItemsQuery
	: IQuery<GetAllTodoItemsQueryAnswer>
	{
		public Guid ListId { get; set; }
		public Guid UserId { get; set; }
	}

	public class GetAllTodoItemsQueryAnswer
	: IQueryAnswer, IMapWith<List<TodoItem>>
	{
		public List<Item> TodoItems { get; init; } = new ();

		public void Map(Profile profile)
			=> profile.CreateMap<List<TodoItem>, GetAllTodoItemsQueryAnswer>()
				.ForMember(destination => destination.TodoItems, options => options.MapFrom(source => source.Select(
					item => new Item()
					{
						Id      = item.Id,
						Name    = item.Name,
						Details = item.Details
					}
				)));
		
		public sealed class Item
		{
			public Guid         Id { get; set; } = Guid.Empty;
			public string     Name { get; set; } = string.Empty;
			public string? Details { get; set; } = null;
		}
	}

	internal sealed class GetAllTodoItemsQueryHandler
	: IQueryHandler<GetAllTodoItemsQuery, GetAllTodoItemsQueryAnswer>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAllTodoItemsQueryHandler(
			IUnitOfWork unitOfWork,
			IMapper mapper
		)
		{
			this._unitOfWork = unitOfWork;
			this._mapper = mapper;
		}

		public async Task<GetAllTodoItemsQueryAnswer> Handle
			(GetAllTodoItemsQuery query, CancellationToken cancellationToken)
		{
			var targetItems = await _unitOfWork.TodoItemRepository
				.GetByListIdAsync(query.ListId, cancellationToken);

			if (targetItems is null)
			{
				throw new EntityNotFoundException(name: nameof(targetItems), key: nameof(TodoList.UserId));
			}
			if (targetItems.Any() is false)
			{
				throw new EntityNotFoundException(name: nameof(targetItems), key: nameof(TodoList.UserId));
			}

			return _mapper.Map<GetAllTodoItemsQueryAnswer>(targetItems);
		}
	}

	internal sealed class GetAllTodoItemsQueryValidator
	: AbstractValidator<GetAllTodoItemsQuery>
	{
		public GetAllTodoItemsQueryValidator()
		{
			RuleFor(item => item.ListId).NotEmpty();
		}
	}
}
