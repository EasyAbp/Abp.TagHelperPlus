using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.Abp.TagHelperPlus.Pages.Books.Book2.ViewModels
{
    public class SearchBookViewModel
    {
        [Required]
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
        public bool? NullableBoolean { get; set; }

        public bool NotNullBoolean { get; set; }


    }
}