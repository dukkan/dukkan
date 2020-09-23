using Abp.Extensions;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Dukkan.Application.Services.Dto;

namespace Dukkan.Catalog.Dto
{
    public class CategoryTranslationEditDto : NullableIdDto, IEntityTranslationDto, ICustomValidate
    {
        [MaxLength(CategoryConsts.NameMaxLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public bool IsTranslatable() => !Name.IsNullOrWhiteSpace() || !Description.IsNullOrWhiteSpace();

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (Language.IsNullOrEmpty() && Name.IsNullOrEmpty())
                context.Results.Add(new ValidationResult("The Name field is required"));
        }
    }
}
