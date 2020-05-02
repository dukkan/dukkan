using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dukkan.Catalog
{
    [Table("CategoryTranslations")]
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