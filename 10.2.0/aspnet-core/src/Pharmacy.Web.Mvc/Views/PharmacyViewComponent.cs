using Abp.AspNetCore.Mvc.ViewComponents;

namespace Pharmacy.Web.Views;

public abstract class PharmacyViewComponent : AbpViewComponent
{
    protected PharmacyViewComponent()
    {
        LocalizationSourceName = PharmacyConsts.LocalizationSourceName;
    }
}
