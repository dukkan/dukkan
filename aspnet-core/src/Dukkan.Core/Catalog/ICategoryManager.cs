using System.Collections.Generic;
using Abp.Domain.Services;

namespace Dukkan.Catalog
{
    public interface ICategoryManager : IDomainService
    {
        IList<Category> SortCategoriesForTree(IList<Category> source, int parentId = 0,
            bool ignoreCategoriesWithoutExistingParent = false);

        string GetFormattedBreadCrumb(Category category, IList<Category> allCategories = null,
            string separator = ">>", string language = null);

        IList<Category> GetCategoryBreadCrumb(Category category, IList<Category> allCategories = null,
            bool showHidden = false);
    }
}