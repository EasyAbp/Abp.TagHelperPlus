using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using EasyAbp.Abp.TagHelperPlus.Pages.Books.Book3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.Abp.TagHelperPlus.Pages.Books.Book3
{
    public class IndexModel : AbpPageModel
    {
        [BindProperty]
        [EasySelector(
            getListedDataSourceUrl: "/api/identity/users",
            getSingleDataSourceUrl: "/api/identity/users/{id}",
            keyPropertyName: "id",
            textPropertyName: "name",
            alternativeTextPropertyName: "userName",
            hideSubText: false,
            runScriptOnWindowLoad: true // Please set to true if the item is not in a modal.
        )]
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public virtual void OnGetAsync()
        {
        }
    }
}