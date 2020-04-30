using Abp.Extensions;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;
using Dukkan.Catalog.Domain;

namespace Dukkan.Catalog.Dto
{
    public class CategoryTranslationEditDto : ICustomValidate
    {
        [MaxLength(CategoryTranslation.NameMaxLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public bool IsDefault { get; set; }

        public bool IsDirty() => !Name.IsNullOrEmpty() || !Description.IsNullOrEmpty();

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (IsDefault && Name.IsNullOrEmpty())
                context.Results.Add(new ValidationResult("The Name field is required"));

            if (!IsDefault && Language.IsNullOrEmpty())
                context.Results.Add(new ValidationResult("The Language field is required"));
        }
    }
}
