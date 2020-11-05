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
        [EasySelector("/api/identity/users", "/api/identity/users/{id}", "id", "name", "userName")]
        public Guid UserId { get; set; }
    }
}