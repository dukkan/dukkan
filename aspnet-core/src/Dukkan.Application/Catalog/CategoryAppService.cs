using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using Dukkan.Catalog.Dto;
using Dukkan.ObjectMapping;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.Catalog
{
    public class CategoryAppService : DukkanAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICategoryManager _categoryManager;
        private readonly ILanguageManager _languageManager;

        public CategoryAppService(IRepository<Category> categoryRepository,
            ICategoryManager categoryManager,
            ILanguageManager languageManager)
        {
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
            _languageManager = languageManager;
        }

        private IQueryable<Category> CreateCategoryQuery(bool includeTranslations = true)
        {
            var query = _categoryRepository.GetAll();

            if (includeTranslations)
                query = query.Include(x => x.Translations);

            return query;
        }

        private static IQueryable<Category> ApplyCategoryFilter(IQueryable<Category> query, CategoryGetAllPagedInput input)
        {
            if (input == null)
                return query;

            query = query.WhereIf(!input.MasterFilter.IsNullOrEmpty(),
                x => x.Translations.Any(y => y.Name.Contains(input.MasterFilter) ||
                                             y.Description.Contains(input.MasterFilter)));

            return query;
        }

        private List<CategoryListDto> ConvertToCategoryListDtos(IEnumerable<Category> entities)
        {
            return entities.Select(category =>
            {
                //fill in model values from the entity
                var categoryModel = ObjectMapper.Map<CategoryListDto>(category);

                //fill in additional values (not existing in the entity)
                categoryModel.Breadcrumb = _categoryManager.GetFormattedBreadCrumb(category, language: _languageManager.CurrentLanguage.Name);

                return categoryModel;
            }).ToList();
        }

        private async Task AddCategoryAsync(CategoryEditDto input)
        {
            var entity = ObjectMapper.Map<Category>(input);

            ObjectMapper.MapMultiLingualEntityTranslations(input.Translations, entity);

            await _categoryRepository.InsertAsync(entity);
        }

        private async Task EditCategoryAsync(CategoryEditDto input)
        {
            var entity = await _categoryRepository.GetAllIncluding(x => x.Translations)
                .FirstOrDefaultAsync(x => x.Id == input.Id);

            ObjectMapper.Map(input, entity);

            ObjectMapper.MapMultiLingualEntityTranslations(input.Translations, entity);
        }

        public async Task<PagedResultDto<CategoryListDto>> GetAllPagedAsync(CategoryGetAllPagedInput input)
        {
            var query = CreateCategoryQuery()
                .OrderBy(c => c.ParentCategoryId)
                .ThenBy(c => c.DisplayOrder)
                .ThenBy(c => c.Id);

            var filteredQuery = ApplyCategoryFilter(query, input);

            var totalCount = await filteredQuery.CountAsync();

            var unsortedEntities = await filteredQuery
                .OrderBy(input.Sorting ?? "id desc")
                .PageBy(input)
                .ToListAsync();

            var sortedCategories = _categoryManager.SortCategoriesForTree(unsortedEntities);

            var dtos = ConvertToCategoryListDtos(sortedCategories);

            return new PagedResultDto<CategoryListDto>(
                totalCount,
                dtos
            );
        }

        public async Task<ListResultDto<CategoryListDto>> GetAllAsync()
        {
            var query = CreateCategoryQuery()
                .OrderBy(c => c.ParentCategoryId)
                .ThenBy(c => c.DisplayOrder)
                .ThenBy(c => c.Id);

            var unsortedEntities = await query.ToListAsync();

            var sortedCategories = _categoryManager.SortCategoriesForTree(unsortedEntities);

            var dtos = ConvertToCategoryListDtos(sortedCategories);

            return new ListResultDto<CategoryListDto>(dtos);
        }

        public async Task<CategoryEditDto> GetForEditAsync(EntityDto input)
        {
            var entity = await _categoryRepository.GetAllIncluding(x => x.Translations)
                .FirstOrDefaultAsync(x => x.Id == input.Id);

            return ObjectMapper.Map<CategoryEditDto>(entity);
        }

        public async Task AddOrEditAsync(CategoryEditDto input)
        {
            if (!input.Id.HasValue)
            {
                await AddCategoryAsync(input);
            }
            else
            {
                await EditCategoryAsync(input);
            }
        }

        public async Task RemoveAsync(EntityDto input)
        {
            await _categoryRepository.DeleteAsync(input.Id);
        }

        //public List<ComboboxItemDto> GetCategoryList(bool showHidden = false)
        //{
        //    var categories = GetAllCategories(showHidden: showHidden);
        //    var listItems = categories.Select(c => new ComboboxItemDto
        //    {
        //        DisplayText = _categoryManager.GetFormattedBreadCrumb(c, categories),
        //        Value = c.Id.ToString()
        //    });

        //    var result = new List<ComboboxItemDto>();
        //    //clone the list to ensure that "selected" property is not set
        //    foreach (var item in listItems)
        //    {
        //        result.Add(new ComboboxItemDto
        //        {
        //            DisplayText = item.Text,
        //            Value = item.Value
        //        });
        //    }

        //    return result;
        //}
    }
}