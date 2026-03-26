using Pharmacy.Configuration.Dto;
using System.Threading.Tasks;

namespace Pharmacy.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
