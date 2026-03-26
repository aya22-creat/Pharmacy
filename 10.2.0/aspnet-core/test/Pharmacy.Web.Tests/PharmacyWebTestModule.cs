using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pharmacy.EntityFrameworkCore;
using Pharmacy.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Pharmacy.Web.Tests;

[DependsOn(
    typeof(PharmacyWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class PharmacyWebTestModule : AbpModule
{
    public PharmacyWebTestModule(PharmacyEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(PharmacyWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(PharmacyWebMvcModule).Assembly);
    }
}