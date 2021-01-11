using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.Abp.TagHelperPlus.Pages.Books.Book
{
    public class IndexModel : AbpPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid? UserId { get; set; }

        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
