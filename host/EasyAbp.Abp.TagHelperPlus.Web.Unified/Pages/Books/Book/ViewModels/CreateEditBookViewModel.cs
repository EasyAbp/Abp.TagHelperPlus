using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;

namespace EasyAbp.Abp.TagHelperPlus.Pages.Books.Book.ViewModels
{
    public class CreateEditBookViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EasySelector(
            getListedDataSourceUrl: "/api/identity/users",
            getSingleDataSourceUrl: "/api/identity/users/{id}",
            keyPropertyName: "id",
            textPropertyName: "name",
            alternativeTextPropertyName: "userName",
            hideSubText: false)]
        public Guid UserId { get; set; }
    }
}