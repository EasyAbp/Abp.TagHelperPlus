﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Json;
using Volo.Abp.Localization;

namespace EasyAbp.Abp.TagHelperPlus.TagHelpers
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAbpTagHelperService<AbpSelectTagHelper>), typeof(AbpSelectTagHelperService))]
    public class TagHelperPlusAbpSelectTagHelperService : AbpSelectTagHelperService
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly AbpRemoteServiceOptions _remoteServiceOptions;

        public TagHelperPlusAbpSelectTagHelperService(
            IHtmlGenerator generator,
            HtmlEncoder encoder,
            IAbpTagHelperLocalizer tagHelperLocalizer,
            IStringLocalizerFactory stringLocalizerFactory,
            IAbpEnumLocalizer abpEnumLocalizer,
            IJsonSerializer jsonSerializer,
            IOptions<AbpRemoteServiceOptions> remoteServiceOptions) : base(generator,
            encoder, tagHelperLocalizer, stringLocalizerFactory, abpEnumLocalizer)
        {
            _jsonSerializer = jsonSerializer;
            _remoteServiceOptions = remoteServiceOptions.Value;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();

            var innerHtml = await GetFormInputGroupAsHtmlAsync(context, output, childContent);

            var order = TagHelper.AspFor.ModelExplorer.GetDisplayOrder();

            AddGroupToFormGroupContents(context, TagHelper.AspFor.Name, SurroundInnerHtmlAndGet(context, output, innerHtml), order, out var suppress);

            if (suppress)
            {
                output.SuppressOutput();
            }
            else
            {
                output.TagName = "div";
                LeaveOnlyGroupAttributes(context, output);
                output.Attributes.AddClass("form-group");
                output.TagMode = TagMode.StartTagAndEndTag;

                var easySelectorAttribute = TagHelper.AspFor.ModelExplorer.GetAttribute<EasySelectorAttribute>();
                if (easySelectorAttribute != null)
                {
                    innerHtml = GetSelect2ConfigurationCode(context, easySelectorAttribute) +
                                   Environment.NewLine +
                                   innerHtml;
                }

                output.Content.SetHtmlContent(innerHtml);
            }
        }

        protected override List<SelectListItem> GetSelectItems(TagHelperContext context, TagHelperOutput output)
        {
            var easySelectorAttribute = TagHelper.AspFor.ModelExplorer.GetAttribute<EasySelectorAttribute>();

            return easySelectorAttribute != null ? new List<SelectListItem>() : base.GetSelectItems(context, output);
        }

        protected override string SurroundInnerHtmlAndGet(TagHelperContext context, TagHelperOutput output, string innerHtml)
        {
            var easySelectorAttribute = TagHelper.AspFor.ModelExplorer.GetAttribute<EasySelectorAttribute>();

            if (easySelectorAttribute == null)
            {
                return base.SurroundInnerHtmlAndGet(context, output, innerHtml);
            }

            return "<div class=\"mb-3 form-group\">" +
                   Environment.NewLine +
                   GetSelect2ConfigurationCode(context, easySelectorAttribute) +
                   Environment.NewLine +
                   innerHtml +
                   Environment.NewLine +
                   "</div>";
        }

        protected virtual string GetSelect2ConfigurationCode(TagHelperContext context, EasySelectorAttribute easySelectorAttribute)
        {
            var styleCode = GetStyleCode(context, easySelectorAttribute);

            var scriptCode = GetScriptCode(context, easySelectorAttribute);

            return styleCode + scriptCode;
        }

        protected virtual string GetStyleCode(TagHelperContext context, EasySelectorAttribute easySelectorAttribute)
        {
            return $"<style>.select2-selection__rendered{{line-height:48px !important;padding-left:1.25rem !important;}}.select2-selection__clear{{right:10px}} .select2-container .select2-selection--single{{height:48px !important;}}.select2-selection__arrow{{height:48px !important;right:10px !important}} .selection-subtext {{ padding-left: 10px; color: #808080 !important; font-size: smaller; }}</style>";
        }

        protected virtual string GetScriptCode(TagHelperContext context, EasySelectorAttribute easySelectorAttribute)
        {
            var currentValues = context.Items.First(x => !(x.Key is string)).Value;

            var placeHolder = TagHelper.AspFor.Metadata.Placeholder;

            var tagId = TagHelper.AspFor.Name.Replace('.', '_');

            var subTextContent = easySelectorAttribute.HideSubText
                ? ""
                : " + '<a class=\"selection-subtext\">' + state.id + '</a>'";

            var configuration = easySelectorAttribute.ModuleName is not null
                ? _remoteServiceOptions.RemoteServices.GetConfigurationOrDefaultOrNull(easySelectorAttribute.ModuleName)
                : null;

            var dropdownParent = easySelectorAttribute.RunScriptOnWindowLoad ? "" : $"dropdownParent: $('#{tagId}').parent().parent(),";

            var transport = easySelectorAttribute.EnableCache ? $",transport:function(params,success,failure){{if(!params.data.{easySelectorAttribute.FilterParamName}&&params.data.skipCount<{tagId.ToCamelCase()}cacheList.length){{return success({{totalCount:{tagId.ToCamelCase()}cacheList.length,{easySelectorAttribute.ItemListPropertyName}:{tagId.ToCamelCase()}cacheList}});}}if(params.data.{easySelectorAttribute.FilterParamName}&&{tagId.ToCamelCase()}cacheList.length&&params.data.skipCount<params.data.maxResultCount){{let matchList={tagId.ToCamelCase()}cacheList.filter(function(item){{return!params.data.{easySelectorAttribute.FilterParamName}||stringMatch(params.data.{easySelectorAttribute.FilterParamName},item.name??item.userName);}});if(matchList.length>params.data.skipCount)return success({{totalCount:params.data.maxResultCount*2,{easySelectorAttribute.ItemListPropertyName}:matchList}});}}if(params.data.skipCount != 0 && {tagId.ToCamelCase()}cacheList.length > params.data.skipCount - params.data.maxResultCount){{params.data.skipCount -= params.data.maxResultCount;}}var $request=$.ajax(params);$request.then((res)=>{{res.{easySelectorAttribute.ItemListPropertyName} = res.{easySelectorAttribute.ItemListPropertyName}.filter(v => {{ return {tagId.ToCamelCase()}cacheList.every(e => e.{easySelectorAttribute.KeyPropertyName} != v.{easySelectorAttribute.KeyPropertyName}); }});{tagId.ToCamelCase()}cacheList = [...{tagId.ToCamelCase()}cacheList, ...res.{easySelectorAttribute.ItemListPropertyName}];success(res);}});$request.fail(failure);return $request;}}" : "";

            var baseUrl = configuration?.BaseUrl;
            var innerCode = $"$(function () {{ let {tagId.ToCamelCase()}cacheList = [];let currentValues = {_jsonSerializer.Serialize(currentValues)}; function stringMatch(term,candidate){{return candidate&&candidate.toLowerCase().indexOf(term.toLowerCase())>=0}}; function matchCustom(params,data){{if($.trim(params.term)===\"\"){{return data}}if(typeof data.text===\"undefined\"){{return null}}if(stringMatch(params.term,data.text)){{return data}}if(stringMatch(params.term,state.id)){{return data}}return null}}; let select2Item = function (state) {{ return $('<span>' + state.text{subTextContent} + '</span>'); }}; let select2Option = {{ allowClear: true,minimumInputLength:{easySelectorAttribute.MinimumInputLength}, width: \"100%\", matcher: matchCustom, templateResult: select2Item, templateSelection: select2Item,{dropdownParent} ajax: {{ url: '{baseUrl}{easySelectorAttribute.GetListedDataSourceUrl}', dataType: \"json\", delay: {easySelectorAttribute.Delay}, data: function (params) {{ params.page = params.page || 1; return {{ {easySelectorAttribute.FilterParamName}: params.term, skipCount: (params.page - 1) * {easySelectorAttribute.MaxResultCount}, maxResultCount: {easySelectorAttribute.MaxResultCount}, }} }}{transport}, processResults: function (data, params) {{ params.page = params.page || 1; return {{ results: data.{easySelectorAttribute.ItemListPropertyName}.map(function (item) {{ return {{ id: item.{easySelectorAttribute.KeyPropertyName}, text: item.{easySelectorAttribute.TextPropertyName} ?? item.{easySelectorAttribute.AlternativeTextPropertyName} }} }}), pagination: {{ more: (params.page * {easySelectorAttribute.MaxResultCount}) < data.totalCount }} }}; }}, cache: true }}, placeholder: {{ id: '', text: '{placeHolder}' }} }}; $(\"#{tagId}\").select2(select2Option); currentValues && currentValues.values.forEach(function(e) {{ if (!$(\"#{tagId}\").find('option:contains(' + e + ')').length && (e != \"00000000-0000-0000-0000-000000000000\" && e != \"\" && e != \"0\")) abp.ajax({{ type: 'GET', url: '{baseUrl}{easySelectorAttribute.GetSingleDataSourceUrl}'.replace('{{id}}', e), success: function (result) {{ $(\"#{tagId}\").append($('<option value=\"' + e + '\">').text(result.{easySelectorAttribute.TextPropertyName} ?? result.{easySelectorAttribute.AlternativeTextPropertyName})); $(\"#{tagId}\").val(currentValues.values).trigger('change'); }}}}); }}); }});";

            return easySelectorAttribute.RunScriptOnWindowLoad
                ? $"<script>window.addEventListener('load', function() {{{innerCode}}}, false)</script>"
                : $"<script>{innerCode}</script>";
        }
    }
}