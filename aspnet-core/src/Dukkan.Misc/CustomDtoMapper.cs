using Abp.AutoMapper;
using AutoMapper;
using Dukkan.Authorization.Users.Domain;
using Dukkan.MultiTenancy.Domain;
using Dukkan.Sessions.Dto;

namespace Dukkan
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression config, MultiLingualMapContext multiLingualMapContext)
        {
            CreateSessionsMaps(config, multiLingualMapContext);
        }

        private static void CreateSessionsMaps(IMapperConfigurationExpression configuration, MultiLingualMapContext multiLingualMapContext)
        {
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
        }
    }
}