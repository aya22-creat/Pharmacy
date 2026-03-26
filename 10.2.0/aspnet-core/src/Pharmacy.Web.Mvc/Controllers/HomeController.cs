using Abp.AspNetCore.Mvc.Authorization;
using Pharmacy.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Web.Controllers;

[AbpMvcAuthorize]
public class HomeController : PharmacyControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
