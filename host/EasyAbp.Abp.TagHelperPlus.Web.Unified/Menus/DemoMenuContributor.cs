using System.Threading.Tasks;
using EasyAbp.Abp.TagHelperPlus.MultiTenancy;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.Abp.TagHelperPlus.Menus
{
    public class DemoMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            if (!MultiTenancyConsts.IsEnabled)
            {
                var administration = context.Menu.GetAdministration();
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            context.Menu.Items.Insert(0, new ApplicationMenuItem("Home", "Home", "~/"));
            
                context.Menu.AddItem(new ApplicationMenuItem("Book", "Book", "/Books/Book"));
        }
    }
}
