using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pharmacy.Configuration;
using Pharmacy.EntityFrameworkCore;
using Pharmacy.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace Pharmacy.Migrator;

[DependsOn(typeof(PharmacyEntityFrameworkModule))]
public class PharmacyMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public PharmacyMigratorModule(PharmacyEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(PharmacyMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            PharmacyConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(PharmacyMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
