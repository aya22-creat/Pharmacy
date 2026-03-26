using Abp.Application.Services;
using Pharmacy.MultiTenancy.Dto;

namespace Pharmacy.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

