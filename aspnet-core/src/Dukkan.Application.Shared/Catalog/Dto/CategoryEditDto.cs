using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dukkan.Application.Services.Dto;

namespace Dukkan.Catalog.Dto
{
    public class CategoryEditDto : NullableIdDto, IMultiLingualEntityDto<CategoryTranslationEditDto>
    {
        public int ParentCategoryId { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        [Required]
        public ICollection<CategoryTranslationEditDto> Translations { get; set; }
    }
}
