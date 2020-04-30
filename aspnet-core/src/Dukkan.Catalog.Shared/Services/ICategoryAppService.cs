using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Dukkan.Catalog.Dto;

namespace Dukkan.Catalog.Services
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<PagedResultDto<CategoryListDto>> GetAllPagedAsync(CategoryGetAllPagedInput input);

        Task<CategoryEditDto> GetForEditAsync(EntityDto input);

        Task AddOrEditAsync(CategoryEditDto input);

        Task RemoveAsync(EntityDto input);
    }
}
