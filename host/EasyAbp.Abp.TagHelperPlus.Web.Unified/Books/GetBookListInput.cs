using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    public class GetBookListInput : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }
    }
}