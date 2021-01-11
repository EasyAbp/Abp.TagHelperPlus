using System.Threading.Tasks;
using EasyAbp.Abp.TagHelperPlus.Pages.Books.Book2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.Abp.TagHelperPlus.Pages.Books.Book2
{
    public class IndexModel : AbpPageModel
    {
        [BindProperty]
        public SearchBookViewModel ViewModel { get; set; }
    }
}
