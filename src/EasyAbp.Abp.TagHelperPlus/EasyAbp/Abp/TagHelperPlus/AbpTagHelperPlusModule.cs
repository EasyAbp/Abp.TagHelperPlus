using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.TagHelperPlus
{
    [DependsOn(typeof(AbpAspNetCoreMvcUiBootstrapModule))]
    public class AbpTagHelperPlusModule : AbpModule
    {

    }
}
