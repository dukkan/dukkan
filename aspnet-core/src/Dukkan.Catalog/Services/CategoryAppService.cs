using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Dukkan.Catalog.Domain;
using Dukkan.Catalog.Dto;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.Catalog.Services
{
    public class CategoryAppService : AbpServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryAppService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
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
                x => x.Translations.Any(y => y.Name.Contains(input.MasterFilter)
                || y.Description.Contains(input.MasterFilter)));

            return query;
        }

        private List<CategoryListDto> ConvertToCategoryListDtos(IEnumerable<Category> entities)
        {
            return ObjectMapper.Map<List<CategoryListDto>>(entities);
        }

        private void TranslateCategory(IEnumerable<CategoryTranslationEditDto> editDtos, Category category)
        {
            foreach (var editDto in editDtos)
            {
                var translation = category.Translations?.FirstOrDefault(x => x.Language == editDto.Language);
                if (translation == null)//insert
                {
                    if (!editDto.IsDirty())
                        continue;

                    translation = ObjectMapper.Map<CategoryTranslation>(editDto);
                    category.Translations.Add(translation);
                }
                else
                {
                    if (editDto.IsDefault)
                    {
                        //! always update
                        ObjectMapper.Map(editDto, translation);
                        continue;
                    }

                    if (editDto.IsDirty())//update
                    {
                        ObjectMapper.Map(editDto, translation);
                    }
                    else//delete
                    {
                        if (category.Translations.Count > 1)//at least one translation required
                            category.Translations.Remove(translation);
                    }
                }
            }
        }

        private async Task AddCategoryAsync(CategoryEditDto input)
        {
            var entity = ObjectMapper.Map<Category>(input);

            TranslateCategory(input.Translations, entity);

            await _categoryRepository.InsertAsync(entity);
        }

        private async Task EditCategoryAsync(CategoryEditDto input)
        {
            var entity = await _categoryRepository.GetAllIncluding(x => x.Translations)
                .FirstOrDefaultAsync(x => x.Id == input.Id);

            ObjectMapper.Map(input, entity);

            TranslateCategory(input.Translations, entity);
        }

        public async Task<PagedResultDto<CategoryListDto>> GetAllPagedAsync(CategoryGetAllPagedInput input)
        {
            var query = CreateCategoryQuery();
            var filteredQuery = ApplyCategoryFilter(query, input);

            var totalCount = await filteredQuery.CountAsync();

            var entities = await filteredQuery
                .OrderBy(input.Sorting ?? "id desc")
                .PageBy(input)
                .ToListAsync();

            var dtos = ConvertToCategoryListDtos(entities);

            return new PagedResultDto<CategoryListDto>(
                 totalCount,
                 dtos
             );
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
    }
}