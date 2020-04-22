using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Dukkan.Configuration.Dto;

namespace Dukkan.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DukkanAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
