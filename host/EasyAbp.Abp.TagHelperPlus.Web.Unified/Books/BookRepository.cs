using System;
using EasyAbp.Abp.TagHelperPlus.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    public class BookRepository : EfCoreRepository<UnifiedDbContext, Book, Guid>, IBookRepository
    {
        public BookRepository(IDbContextProvider<UnifiedDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}