using System.Collections.Generic;

namespace Dukkan.Application.Services.Dto
{
    public interface IMultiLingualEntityDto<TTranslation> where TTranslation : class, IEntityTranslationDto
    {
        ICollection<TTranslation> Translations { get; set; }
    }
}