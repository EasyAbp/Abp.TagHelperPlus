using System;
using JetBrains.Annotations;

namespace EasyAbp.Abp.TagHelperPlus.EasySelector
{
    public class EasySelectorAttribute : Attribute
    {
        /// <summary>
        /// Url to get listed data.
        /// </summary>
        /// <example>/api/identity/users</example>
        public string GetListedDataSourceUrl { get; set; }
        
        /// <summary>
        /// Url to get single data.
        /// Use <code>{id}`</code> as the key.
        /// </summary>
        /// <example>/api/identity/users/{id}</example>
        public string GetSingleDataSourceUrl { get; set; }

        /// <summary>
        /// Used to be the value of the option.
        /// </summary>
        public string KeyPropertyName { get; set; }

        /// <summary>
        /// Used to be the display text of the option.
        /// </summary>
        public string TextPropertyName { get; set; }
        
        /// <summary>
        /// Used to be the display text of the option if the text property is null.
        /// </summary>
        public string AlternativeTextPropertyName { get; set; }

        /// <summary>
        /// Name of the item list property in the get-list result.
        /// </summary>
        public string ItemListPropertyName { get; set; }

        /// <summary>
        /// The remote service base URL of the module will be add to every URLs as a prefix.
        /// </summary>
        public string ModuleName { get; set; }

        public int MaxResultCount { get; set; }

        /// <summary>
        /// Hide the sub text (key) of items.
        /// </summary>
        public bool HideSubText { get; set; }
        
        /// <summary>
        /// Please set to true if the item is not in a modal.
        /// </summary>
        public bool RunScriptOnWindowLoad { get; set; }
        
        public EasySelectorAttribute(
            [NotNull] string getListedDataSourceUrl,
            [NotNull] string getSingleDataSourceUrl,
            [NotNull] string keyPropertyName = "id",
            [NotNull] string textPropertyName = "id",
            [CanBeNull] string alternativeTextPropertyName = "id",
            [NotNull] string itemListPropertyName = "items",
            [CanBeNull] string moduleName = null,
            int maxResultCount = 10,
            bool hideSubText = false,
            bool runScriptOnWindowLoad = false)
        {
            GetListedDataSourceUrl = getListedDataSourceUrl;
            GetSingleDataSourceUrl = getSingleDataSourceUrl;
            KeyPropertyName = keyPropertyName;
            TextPropertyName = textPropertyName;
            AlternativeTextPropertyName = alternativeTextPropertyName;
            ItemListPropertyName = itemListPropertyName;
            ModuleName = moduleName;
            MaxResultCount = maxResultCount;
            HideSubText = hideSubText;
            RunScriptOnWindowLoad = runScriptOnWindowLoad;
        }
    }
}