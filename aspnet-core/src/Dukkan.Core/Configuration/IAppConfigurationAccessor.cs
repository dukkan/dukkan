using Microsoft.Extensions.Configuration;

namespace Dukkan.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
