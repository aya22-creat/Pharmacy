using Abp.Authorization;
using Abp.Runtime.Session;
using Pharmacy.Configuration.Dto;
using System.Threading.Tasks;

namespace Pharmacy.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : PharmacyAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
