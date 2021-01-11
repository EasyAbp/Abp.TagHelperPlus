using System;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    public class BookAppService : ReadOnlyAppService<Book, BookDto, Guid, GetBookListInput>, IBookAppService
    {
        public BookAppService(IReadOnlyRepository<Book, Guid> repository) : base(repository)
        {
        }

        protected override IQueryable<Book> CreateFilteredQuery(GetBookListInput input)
        {
            return ReadOnlyRepository.WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId.Value);
        }

        protected override BookDto MapToGetOutputDto(Book entity)
        {
            return new BookDto
            {
                Id =  entity.Id,
                Name = entity.Name,
                UserId = entity.UserId
            };
        }

        protected override BookDto MapToGetListOutputDto(Book entity)
        {
            return MapToGetOutputDto(entity);
        }
    }
}