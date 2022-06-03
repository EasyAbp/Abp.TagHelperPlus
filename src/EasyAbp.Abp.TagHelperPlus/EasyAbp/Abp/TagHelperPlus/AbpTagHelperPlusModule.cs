using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.TagHelperPlus;

[DependsOn(
    typeof(AbpHttpClientModule),
    typeof(AbpAspNetCoreMvcUiBootstrapModule)
)]
public class AbpTagHelperPlusModule : AbpModule
{

}