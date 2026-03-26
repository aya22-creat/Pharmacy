using Abp.AspNetCore.Mvc.Authorization;
using Pharmacy.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Web.Controllers;

[AbpMvcAuthorize]
public class AboutController : PharmacyControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
