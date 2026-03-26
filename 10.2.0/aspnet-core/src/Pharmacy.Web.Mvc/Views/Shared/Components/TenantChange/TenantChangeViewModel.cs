using Abp.AutoMapper;
using Pharmacy.Sessions.Dto;

namespace Pharmacy.Web.Views.Shared.Components.TenantChange;

[AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
public class TenantChangeViewModel
{
    public TenantLoginInfoDto Tenant { get; set; }
}
