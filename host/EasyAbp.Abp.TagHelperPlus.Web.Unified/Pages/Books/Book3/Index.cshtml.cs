using System.Threading.Tasks;
using EasyAbp.Abp.TagHelperPlus.Pages.Books.Book3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.Abp.TagHelperPlus.Pages.Books.Book3
{
    public class IndexModel : AbpPageModel
    {
        [BindProperty]
        public SearchBookViewModel ViewModel { get; set; }

        public virtual void OnGetAsync()
        {
            ViewModel = new SearchBookViewModel();
        }
    }
}
