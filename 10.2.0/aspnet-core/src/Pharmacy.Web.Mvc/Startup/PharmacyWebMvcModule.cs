using Abp.Modules;
using Abp.Reflection.Extensions;
using Pharmacy.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Pharmacy.Web.Startup;

[DependsOn(typeof(PharmacyWebCoreModule))]
public class PharmacyWebMvcModule : AbpModule
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfigurationRoot _appConfiguration;

    public PharmacyWebMvcModule(IWebHostEnvironment env)
    {
        _env = env;
        _appConfiguration = env.GetAppConfiguration();
    }

    public override void PreInitialize()
    {
        Configuration.Navigation.Providers.Add<PharmacyNavigationProvider>();
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(PharmacyWebMvcModule).GetAssembly());
    }
}
