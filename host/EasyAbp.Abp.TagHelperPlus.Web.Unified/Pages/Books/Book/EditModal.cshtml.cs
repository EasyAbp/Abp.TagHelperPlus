using System;
using System.Threading.Tasks;
using EasyAbp.Abp.TagHelperPlus.Books;
using EasyAbp.Abp.TagHelperPlus.Pages.Books.Book.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.Abp.TagHelperPlus.Pages.Books.Book
{
    public class EditModalModel : AbpPageModel
    {
        private readonly IBookRepository _repository;

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditBookViewModel ViewModel { get; set; }


        public EditModalModel(IBookRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task OnGetAsync()
        {
            var book = await _repository.GetAsync(Id);
            
            ViewModel = new CreateEditBookViewModel
            {
                Name = book.Name,
                UserId = book.UserId
            };
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var book = await _repository.GetAsync(Id);

            book.Name = ViewModel.Name;
            book.UserId = ViewModel.UserId;

            await _repository.UpdateAsync(book, true);
            
            return NoContent();
        }
    }
}