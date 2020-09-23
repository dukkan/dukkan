using System.Linq;
using Abp;
using Abp.Domain.Entities;
using Abp.ObjectMapping;
using Dukkan.Application.Services.Dto;

namespace Dukkan.ObjectMapping
{
    public static class DukkanObjectMapperExtensions
    {
        public static void MapMultiLingualEntityTranslations<TTranslation, TTranslationDto>(this IObjectMapper mapper,
            IMultiLingualEntityDto<TTranslationDto> source,
            IMultiLingualEntity<TTranslation> destination)
            where TTranslationDto : class, IEntityTranslationDto
            where TTranslation : class, IEntityTranslation
        {
            Check.NotNull(source, nameof(source));
            Check.NotNullOrEmpty(source.Translations, nameof(source.Translations));
            Check.NotNull(destination, nameof(destination));

            foreach (var translationDto in source.Translations)
            {
                var translation = destination.Translations.FirstOrDefault(x => x.Language == translationDto.Language);
                if (translation != null)
                {
                    if (!translationDto.IsTranslatable())
                    {
                        //delete
                        destination.Translations.Remove(translation);
                    }
                    else
                    {
                        //update
                        mapper.Map(translationDto, translation);
                    }
                }
                else
                {
                    if (!translationDto.IsTranslatable())
                        continue;

                    //insert
                    translation = mapper.Map<TTranslation>(translationDto);
                    destination.Translations.Add(translation);
                }
            }
        }
    }
}