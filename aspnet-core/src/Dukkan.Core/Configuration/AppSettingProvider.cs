using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Microsoft.Extensions.Configuration;

namespace Dukkan.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AppSettingProvider(IAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return GetFooSettings()
                .Union(GetBarSettings())
                .Union(GetBazSettings());
        }

        private IEnumerable<SettingDefinition> GetFooSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettings.Foo.Setting1, "false"),
                new SettingDefinition(AppSettings.Foo.Setting1, "true")
            };
        }

        private IEnumerable<SettingDefinition> GetBarSettings()
        {
            return Enumerable.Empty<SettingDefinition>();
        }

        private IEnumerable<SettingDefinition> GetBazSettings()
        {
            return Enumerable.Empty<SettingDefinition>();
        }

        private string GetFromAppSettings(string name, string defaultValue = null)
        {
            return GetFromSettings("App:" + name, defaultValue);
        }

        private string GetFromSettings(string name, string defaultValue = null)
        {
            return _appConfiguration[name] ?? defaultValue;
        }
    }
}