using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.TagHelperPlus.EasySelector
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAbpTagHelperService<AbpDynamicFormTagHelper>), typeof(AbpDynamicFormTagHelperService))]
    public class MyAbpDynamicFormTagHelperService : AbpDynamicFormTagHelperService
    {
        private readonly IServiceProvider _serviceProvider;

        public MyAbpDynamicFormTagHelperService(
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

        protected override AbpTagHelper GetSelectGroupTagHelper(TagHelperContext context, TagHelperOutput output, ModelExpression model)
        {
            if (IsRadioGroup(model.ModelExplorer))
            {
                return GetAbpRadioInputTagHelper(model);
            }
            
            if (IsEasySelectorGroup(model.ModelExplorer))
            {
                return GetEasySelectorTagHelper(model);
            }
            
            return GetSelectTagHelper(model);
        }

        protected virtual AbpTagHelper GetEasySelectorTagHelper(ModelExpression model)
        {
            var abpSelectTagHelper = _serviceProvider.GetRequiredService<AbpSelectTagHelper>();
            abpSelectTagHelper.AspFor = model;
            abpSelectTagHelper.AspItems = null;
            abpSelectTagHelper.ViewContext = TagHelper.ViewContext;
            return abpSelectTagHelper;
        }

        protected virtual bool IsEasySelectorGroup(ModelExplorer explorer)
        {
            return explorer.GetAttribute<EasySelectorAttribute>() != null;
        }
    }
}