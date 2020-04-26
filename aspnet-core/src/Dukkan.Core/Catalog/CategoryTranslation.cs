using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Dukkan.Catalog
{
    public class CategoryTranslation : Entity, IEntityTranslation<Category>
    {
        [Required]
        [MaxLength(CategoryConsts.NameMaxLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public Category Core { get; set; }

        public int CoreId { get; set; }

        public string Language { get; set; }
    }
}