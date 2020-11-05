using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    public class BookDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        
        public Guid UserId { get; set; }
    }
}