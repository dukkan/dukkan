using System.Collections.Generic;
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
            ICollection<TTranslationDto> translationDtos,
            IMultiLingualEntity<TTranslation> entity)
            where TTranslationDto : class, IEntityTranslationDto
            where TTranslation : class, IEntityTranslation
        {
            Check.NotNullOrEmpty(translationDtos, nameof(translationDtos));
            Check.NotNull(entity, nameof(entity));

            foreach (var translationDto in translationDtos)
            {
                var translation = entity.Translations.FirstOrDefault(x => x.Language == translationDto.Language);
                if (translation != null)
                {
                    if (!translationDto.IsTranslatable())
                    {
                        //delete
                        entity.Translations.Remove(translation);
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
                    entity.Translations.Add(translation);
                }
            }
        }
    }
}