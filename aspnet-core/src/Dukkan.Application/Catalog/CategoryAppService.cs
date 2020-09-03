using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Dukkan.Catalog.Dto;
using Dukkan.ObjectMapping;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.Catalog
{
    public class CategoryAppService : DukkanAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICategoryManager _categoryManager;

        public CategoryAppService(IRepository<Category> categoryRepository,
            ICategoryManager categoryManager)
        {
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
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
            return entities.Select(entity =>
            {
                var dto = ObjectMapper.Map<CategoryListDto>(entity);
                dto.Breadcrumb = _categoryManager.GetFormattedBreadCrumb(entity);

                return dto;
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

            var sortedEntities = _categoryManager.SortCategoriesForTree(unsortedEntities);

            var dtos = ConvertToCategoryListDtos(sortedEntities);

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

            var sortedEntities = _categoryManager.SortCategoriesForTree(unsortedEntities);

            var dtos = ConvertToCategoryListDtos(sortedEntities);

            return new ListResultDto<CategoryListDto>(dtos);
        }

        public async Task<CategoryEditDto> GetForEditAsync(EntityDto input)
        {
            var entity = await CreateCategoryQuery().FirstOrDefaultAsync(x => x.Id == input.Id);

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
    }
}