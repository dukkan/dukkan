using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Dukkan.Catalog.Domain
{
    [Table("CategoryTranslations")]
    public class CategoryTranslation : Entity, IEntityTranslation<Category>
    {
        public const int NameMaxLength = 400;

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public Category Core { get; set; }

        public int CoreId { get; set; }

        public string Language { get; set; }
    }
}