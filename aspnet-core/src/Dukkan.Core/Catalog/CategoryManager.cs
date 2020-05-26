using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Extensions;

namespace Dukkan.Catalog
{
    public class CategoryManager : DukkanDomainServiceBase, ICategoryManager
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryManager(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
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

            //find categories without parent in provided category source and insert them into result
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
                var categoryName = breadcrumb[i].Translations.FirstOrDefault(x => x.Language == language)?.Name;
                result = result.IsNullOrEmpty() ? categoryName : $"{result} {separator} {categoryName}";
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
