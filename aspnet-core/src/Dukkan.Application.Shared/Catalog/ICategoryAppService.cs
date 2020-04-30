using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Dukkan.Catalog.Dto;
using System.Threading.Tasks;

namespace Dukkan.Catalog
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<PagedResultDto<CategoryListDto>> GetAllPagedAsync(CategoryGetAllPagedInput input);

        Task<CategoryEditDto> GetForEditAsync(EntityDto input);

        Task AddOrEditAsync(CategoryEditDto input);

        Task RemoveAsync(EntityDto input);
    }
}
