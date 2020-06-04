using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Dukkan.EntityFrameworkCore.Seed.Host
{
    public class DefaultLanguagesCreator
    {
        public static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

        private readonly DukkanDbContext _context;

        private static List<ApplicationLanguage> GetInitialLanguages()
        {
            var tenantId = DukkanConsts.MultiTenancyEnabled ? null : (int?)MultiTenancyConsts.DefaultTenantId;
            return new List<ApplicationLanguage>
            {
                new ApplicationLanguage(tenantId, "en", "English", "flag-icon flag-icon-us"),
                new ApplicationLanguage(tenantId, "tr", "Türkçe", "flag-icon flag-icon-tr"),
                new ApplicationLanguage(tenantId, "ar", "العربية", "flag-icon flag-icon-sa"),
                new ApplicationLanguage(tenantId, "ru", "Русский", "flag-icon flag-icon-ru"),
            };
        }

        public DefaultLanguagesCreator(DukkanDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.IgnoreQueryFilters().Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);
            _context.SaveChanges();
        }
    }
}
