using System.Globalization;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Localization;

namespace Dukkan.Domain.Entities
{
    public static class DukkanEntityExtensions
    {
        public static TTranslation GetTranslationOrNull<TTranslation>(this IMultiLingualEntity<TTranslation> entity,
            string language = null)
            where TTranslation : class, IEntityTranslation
        {
            if (entity.Translations.IsNullOrEmpty())
            {
                return null;
            }

            var translation = entity.Translations.FirstOrDefault(x => x.Language == language);
            if (translation != null)
            {
                return translation;
            }

            translation = entity.Translations.FirstOrDefault(x => x.Language == CultureInfo.CurrentUICulture.Name);
            if (translation != null)
            {
                return translation;
            }

            using (var iocManger = IocManager.Instance.CreateScope())
            {
                var settingManager = iocManger.Resolve<ISettingManager>();
                var defaultLanguage = settingManager.GetSettingValue(LocalizationSettingNames.DefaultLanguage);

                translation = entity.Translations.FirstOrDefault(x => x.Language == defaultLanguage);
                if (translation != null)
                {
                    return translation;
                }
            }

            translation = entity.Translations.FirstOrDefault();

            return translation;
        }
    }
}
