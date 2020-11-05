using System;
using System.Threading.Tasks;
using EasyAbp.Abp.TagHelperPlus.Books;
using EasyAbp.Abp.TagHelperPlus.Pages.Books.Book.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.Abp.TagHelperPlus.Pages.Books.Book
{
    public class CreateModalModel : AbpPageModel
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IBookRepository _repository;

        [BindProperty]
        public CreateEditBookViewModel ViewModel { get; set; }

        public CreateModalModel(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IBookRepository repository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _repository = repository;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var book = new TagHelperPlus.Books.Book(_guidGenerator.Create(), ViewModel.Name, ViewModel.UserId);
            
            await _repository.InsertAsync(book, true);
            
            return NoContent();
        }
    }
}