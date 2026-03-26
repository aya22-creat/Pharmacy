using Abp.Modules;
using Abp.Reflection.Extensions;
using Pharmacy.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Pharmacy.Web.Host.Startup
{
    [DependsOn(
       typeof(PharmacyWebCoreModule))]
    public class PharmacyWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public PharmacyWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PharmacyWebHostModule).GetAssembly());
        }
    }
}
