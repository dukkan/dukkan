using Abp.AutoMapper;
using AutoMapper;
using Dukkan.Catalog.Domain;
using Dukkan.Catalog.Dto;

namespace Dukkan.Catalog
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression config, MultiLingualMapContext multiLingualMapContext)
        {
            CreateCatalogMaps(config, multiLingualMapContext);
        }

        private static void CreateCatalogMaps(IMapperConfigurationExpression configuration, MultiLingualMapContext multiLingualMapContext)
        {
            configuration.CreateMap<Category, CategoryListDto>();
            configuration.CreateMap<Category, CategoryEditDto>();
            configuration.CreateMap<CategoryEditDto, Category>()
                .ForMember(x => x.Translations, opt => opt.Ignore());
            configuration.CreateMap<CategoryTranslation, CategoryTranslationEditDto>().ReverseMap();
            configuration.CreateMultiLingualMap<Category, CategoryTranslation, CategoryListDto>(multiLingualMapContext);
        }
    }
}