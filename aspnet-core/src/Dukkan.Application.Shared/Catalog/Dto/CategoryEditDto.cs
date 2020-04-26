using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dukkan.Catalog.Dto
{
    public class CategoryEditDto : NullableIdDto
    {
        public int ParentCategoryId { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        [Required]
        public List<CategoryTranslationEditDto> Translations { get; set; }
    }
}
