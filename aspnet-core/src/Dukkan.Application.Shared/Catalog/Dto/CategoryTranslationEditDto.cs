using Abp.Extensions;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dukkan.Catalog.Dto
{
    public class CategoryTranslationEditDto : ICustomValidate
    {
        [MaxLength(CategoryConsts.NameMaxLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public bool IsDirty() => !Name.IsNullOrEmpty() || !Description.IsNullOrEmpty();

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (Language.IsNullOrEmpty() && Name.IsNullOrEmpty())
                context.Results.Add(new ValidationResult("The Name field is required"));
        }
    }
}
