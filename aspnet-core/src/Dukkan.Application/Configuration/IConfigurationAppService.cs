using System.Threading.Tasks;
using Dukkan.Configuration.Dto;

namespace Dukkan.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
