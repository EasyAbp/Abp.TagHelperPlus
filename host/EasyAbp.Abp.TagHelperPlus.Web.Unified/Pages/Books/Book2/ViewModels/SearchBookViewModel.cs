using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;

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
            runScriptOnWindowLoad: true, // Please set to true if the item is not in a modal.            
            minimumInputLength: 1, // The minimum number of characters required to start a search
            delay: 500, // The delay time between the last key stroke and the request is sent to the server
            enableCache: true // Enable cache to reduce the number of requests
        )]
        public Guid UserId { get; set; }

        public string Title { get; set; }
    }
}