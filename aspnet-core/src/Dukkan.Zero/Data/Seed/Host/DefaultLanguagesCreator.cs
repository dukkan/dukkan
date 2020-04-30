using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.Data.Seed.Host
{
    public class DefaultLanguagesCreator
    {
        public static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

        private readonly DukkanZeroDbContext _context;

        private static List<ApplicationLanguage> GetInitialLanguages()
        {
            var tenantId = DukkanConsts.MultiTenancyEnabled ? null : (int?)MultiTenancyConsts.DefaultTenantId;
            return new List<ApplicationLanguage>
            {
                new ApplicationLanguage(tenantId, "en", "English", "famfamfam-flags gb"),
                new ApplicationLanguage(tenantId, "tr", "Türkçe", "famfamfam-flags tr"),
                new ApplicationLanguage(tenantId, "ar", "العربية", "famfamfam-flags sa"),
                new ApplicationLanguage(tenantId, "ru", "Русский", "famfamfam-flags ru"),
            };
        }

        public DefaultLanguagesCreator(DukkanZeroDbContext context)
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
