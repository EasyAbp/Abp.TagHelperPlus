using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    [RemoteService(Name = "Demo")]
    [Route("/api/demo/book")]
    public class BookController : AbpController, IBookAppService
    {
        private readonly IBookAppService _bookAppService;

        public BookController(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<BookDto> GetAsync(Guid id)
        {
            return await _bookAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<BookDto>> GetListAsync(GetBookListInput input)
        {
            return await _bookAppService.GetListAsync(input);
        }
    }
}