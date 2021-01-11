using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    public interface IBookAppService : IReadOnlyAppService<BookDto, Guid, GetBookListInput>
    {
        
    }
}