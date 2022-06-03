using System;
using System.Text.Encodings.Web;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.TagHelperPlus.TagHelpers
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAbpTagHelperService<AbpDynamicFormTagHelper>), typeof(AbpDynamicFormTagHelperService))]
    public class TagHelperPlusAbpDynamicFormTagHelperService : AbpDynamicFormTagHelperService
    {
        public TagHelperPlusAbpDynamicFormTagHelperService(
            HtmlEncoder htmlEncoder,
            IHtmlGenerator htmlGenerator,
            IServiceProvider serviceProvider,
            IStringLocalizer<AbpUiResource> localizer)
            : base(htmlEncoder, htmlGenerator, serviceProvider, localizer)
        {
        }

        protected override bool IsSelectGroup(TagHelperContext context, ModelExpression model)
        {
            return base.IsSelectGroup(context, model) || IsEasySelectorGroup(model.ModelExplorer);
        }

        protected virtual bool IsEasySelectorGroup(ModelExplorer explorer)
        {
            return explorer.GetAttribute<EasySelectorAttribute>() != null;
        }
    }
}