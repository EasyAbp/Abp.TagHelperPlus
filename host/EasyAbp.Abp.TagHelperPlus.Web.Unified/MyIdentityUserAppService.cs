using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace EasyAbp.Abp.TagHelperPlus
{
    [Dependency(ReplaceServices = true)]
    public class MyIdentityUserAppService : IdentityUserAppService
    {
        public MyIdentityUserAppService(
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IOptions<IdentityOptions> identityOptions,
            IPermissionChecker permissionChecker)
            : base(userManager, userRepository, roleRepository, identityOptions, permissionChecker)
        {
        }

        [AllowAnonymous]
        public override Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
        {
            return base.GetListAsync(input);
        }

        [AllowAnonymous]
        public override Task<IdentityUserDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }
    }
}