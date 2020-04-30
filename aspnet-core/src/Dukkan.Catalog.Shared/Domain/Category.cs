using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Dukkan.Catalog.Domain
{
    [Table("Categories")]
    public class Category : FullAuditedEntity, IMultiLingualEntity<CategoryTranslation>
    {
        private ICollection<CategoryTranslation> _translations;

        public int ParentCategoryId { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        public ICollection<CategoryTranslation> Translations
        {
            get => _translations ??= new List<CategoryTranslation>();
            set => _translations = value;
        }
    }
}