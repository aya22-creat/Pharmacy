using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pharmacy.Authorization;

namespace Pharmacy;

[DependsOn(
    typeof(PharmacyCoreModule),
    typeof(AbpAutoMapperModule))]
public class PharmacyApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<PharmacyAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(PharmacyApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
