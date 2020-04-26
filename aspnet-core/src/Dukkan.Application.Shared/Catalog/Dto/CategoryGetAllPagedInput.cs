using Abp.Application.Services.Dto;

namespace Dukkan.Catalog.Dto
{
    public class CategoryGetAllPagedInput : PagedAndSortedResultRequestDto
    {
        public string MasterFilter { get; set; }
    }
}