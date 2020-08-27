using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Localization;

namespace Dukkan.Catalog
{
    public class CategoryManager : DukkanDomainServiceBase, ICategoryManager
    {
        private readonly IRepository<Category> _categoryRepository;


        public CategoryManager(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private CategoryTranslation GetCategoryTranslation(Category category, string language = null)
        {
            var translation = category.Translations.FirstOrDefault(x => x.Language == language);
            if (translation != null)
            {
                return translation;
            }

            translation = category.Translations.FirstOrDefault(x => x.Language == CultureInfo.CurrentUICulture.Name);
            if (translation != null)
            {
                return translation;
            }

            var defaultLanguage = SettingManager.GetSettingValueForApplication(LocalizationSettingNames.DefaultLanguage);

            translation = category.Translations.FirstOrDefault(pt => pt.Language == defaultLanguage);
            if (translation != null)
            {
                return translation;
            }

            translation = category.Translations.FirstOrDefault();
            if (translation != null)
            {
                return translation;
            }

            throw new Exception("No translation found");
        }

        public IList<Category> SortCategoriesForTree(IList<Category> source, int parentId = 0,
            bool ignoreCategoriesWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var result = new List<Category>();

            foreach (var cat in source.Where(c => c.ParentCategoryId == parentId).ToList())
            {
                result.Add(cat);
                result.AddRange(SortCategoriesForTree(source, cat.Id, true));
            }

            if (ignoreCategoriesWithoutExistingParent || result.Count == source.Count)
                return result;

            //find categories without parent in provided category category and insert them into result
            foreach (var cat in source)
                if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
                    result.Add(cat);

            return result;
        }

        public string GetFormattedBreadCrumb(Category category, IList<Category> allCategories = null,
            string separator = ">>", string language = null)
        {
            var result = string.Empty;

            var breadcrumb = GetCategoryBreadCrumb(category, allCategories, true);
            for (var i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var translation = GetCategoryTranslation(breadcrumb[i], language);
                result = result.IsNullOrEmpty() ? translation.Name : $"{result} {separator} {translation.Name}";
            }

            return result;
        }

        public virtual IList<Category> GetCategoryBreadCrumb(Category category, IList<Category> allCategories = null,
            bool showHidden = false)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var result = new List<Category>();

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>();

            while (category != null && //not null
                   (showHidden || category.Published) && //published
                !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
            {
                result.Add(category);

                alreadyProcessedCategoryIds.Add(category.Id);

                category = allCategories != null
                    ? allCategories.FirstOrDefault(c => c.Id == category.ParentCategoryId)
                    : _categoryRepository.FirstOrDefault(category.ParentCategoryId);
            }

            result.Reverse();
            return result;
        }
    }
}
