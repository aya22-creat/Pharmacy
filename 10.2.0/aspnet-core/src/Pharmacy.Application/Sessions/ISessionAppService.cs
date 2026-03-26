using Abp.Application.Services;
using Pharmacy.Sessions.Dto;
using System.Threading.Tasks;

namespace Pharmacy.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
