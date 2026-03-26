using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Controllers
{
    public abstract class PharmacyControllerBase : AbpController
    {
        protected PharmacyControllerBase()
        {
            LocalizationSourceName = PharmacyConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
