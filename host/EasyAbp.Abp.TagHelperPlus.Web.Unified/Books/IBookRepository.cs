using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        
    }
}