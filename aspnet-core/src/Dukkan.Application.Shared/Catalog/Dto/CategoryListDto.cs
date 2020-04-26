using Abp.Application.Services.Dto;

namespace Dukkan.Catalog.Dto
{
    public class CategoryListDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentCategoryId { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }
    }
}
