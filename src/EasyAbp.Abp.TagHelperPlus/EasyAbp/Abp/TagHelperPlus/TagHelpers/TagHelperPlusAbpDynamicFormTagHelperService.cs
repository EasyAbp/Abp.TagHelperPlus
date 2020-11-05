using System;
using System.Text.Encodings.Web;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        private readonly IServiceProvider _serviceProvider;

        public TagHelperPlusAbpDynamicFormTagHelperService(
            HtmlEncoder htmlEncoder,
            IHtmlGenerator htmlGenerator,
            IServiceProvider serviceProvider)
            : base(htmlEncoder, htmlGenerator, serviceProvider)
        {
            _serviceProvider = serviceProvider;
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