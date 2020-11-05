using EasyAbp.Abp.TagHelperPlus.Books;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace EasyAbp.Abp.TagHelperPlus.EntityFrameworkCore
{
    public class UnifiedDbContext : AbpDbContext<UnifiedDbContext>
    {
        public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigureAuditLogging();
            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureFeatureManagement();
            modelBuilder.ConfigureTenantManagement();

            ConfigureDemoEntities(modelBuilder);
        }

        private static void ConfigureDemoEntities(ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AbpModelBuilderConfigurationOptions("Demo");

            builder.Entity<Book>(b =>
            {
                b.ToTable(options.TablePrefix + "Books", options.Schema);

                b.ConfigureByConvention();
            });
        }
    }
}
