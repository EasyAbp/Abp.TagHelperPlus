using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    public class BookAppService : ReadOnlyAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto>, IBookAppService
    {
        public BookAppService(IReadOnlyRepository<Book, Guid> repository) : base(repository)
        {
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