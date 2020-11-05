using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.Abp.TagHelperPlus.EasySelector
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAbpTagHelperService<AbpSelectTagHelper>), typeof(AbpSelectTagHelperService))]
    public class MyAbpSelectTagHelperService : AbpSelectTagHelperService
    {
        private readonly IJsonSerializer _jsonSerializer;

        public MyAbpSelectTagHelperService(
            IHtmlGenerator generator,
            HtmlEncoder encoder,
            IAbpTagHelperLocalizer tagHelperLocalizer,
            IStringLocalizerFactory stringLocalizerFactory,
            IJsonSerializer jsonSerializer) : base(generator,
            encoder, tagHelperLocalizer, stringLocalizerFactory)
        {
            _jsonSerializer = jsonSerializer;
        }

        protected override List<SelectListItem> GetSelectItems(TagHelperContext context, TagHelperOutput output)
        {
            var easySelectorAttribute = TagHelper.AspFor.ModelExplorer.GetAttribute<EasySelectorAttribute>();

            return easySelectorAttribute != null ? new List<SelectListItem>() : base.GetSelectItems(context, output);
        }
        
        protected override string SurroundInnerHtmlAndGet(TagHelperContext context, TagHelperOutput output, string innerHtml)
        {
            return "<div class=\"form-group\">" +
                   Environment.NewLine +
                   GetSelect2ConfigurationCode(context) +
                   Environment.NewLine +
                   innerHtml +
                   Environment.NewLine +
                   "</div>";
        }
        
        protected virtual string GetSelect2ConfigurationCode(TagHelperContext context)
        {
            var easySelectorAttribute = TagHelper.AspFor.ModelExplorer.GetAttribute<EasySelectorAttribute>();

            var currentValues = context.Items.First(x => !(x.Key is string)).Value;

            var placeHolder = TagHelper.AspFor.Metadata.Placeholder;

            var tagId = TagHelper.AspFor.Name.Replace('.', '_');
            
            return "<script>$(function () { let currentValues = " +
                   _jsonSerializer.Serialize(currentValues) +
                   "; let select2Item = function (item) { return item.text; }; let select2Option = { allowClear: true, width: \"100%\", templateResult: select2Item, templateSelection: select2Item, ajax: { url: '" +
                   easySelectorAttribute.GetListedDataSourceUrl +
                   "', dataType: \"json\", delay: 250, data: function (params) { params.page = params.page || 1; return { filter: params.term, skipCount: (params.page - 1) * " +
                   easySelectorAttribute.MaxResultCount +
                   ", maxResultCount: " +
                   easySelectorAttribute.MaxResultCount +
                   ", } }, processResults: function (data, params) { params.page = params.page || 1; return { results: data.items.map(function (item) { return { id: item." +
                   easySelectorAttribute.KeyPropertyName +
                   ", text: item." +
                   easySelectorAttribute.TextPropertyName +
                   " ?? item." +
                   easySelectorAttribute.AlternativeTextPropertyName +
                   " } }), pagination: { more: (params.page * " +
                   easySelectorAttribute.MaxResultCount +
                   ") < data.totalCount } }; }, cache: true }, placeholder: { id: -1, text: '" +
                   placeHolder +
                   "' } }; $(\"#" +
                   tagId +
                   "\").select2(select2Option); " +
                   "currentValues && currentValues.values.forEach(function(e) { if (!$(\"#" +
                   tagId +
                   "\").find('option:contains(' + e + ')').length) abp.ajax({ type: 'GET', url: '" +
                   easySelectorAttribute.GetSingleDataSourceUrl +
                   "'.replace('{id}', e), success: function (result) { $(\"#" +
                   tagId +
                   "\").append($('<option value=\"' + e + '\">').text(result." +
                   easySelectorAttribute.TextPropertyName +
                   " ?? result." +
                   easySelectorAttribute.AlternativeTextPropertyName +
                   ")); }}); " +
                   "$(\"#" +
                   tagId +
                   "\").val(currentValues.values).trigger('change'); }); " +
                   "});</script>";
        }
    }
}