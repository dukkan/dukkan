using Abp.Collections.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dukkan.Web.Swagger
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            for (var i = 0; i < operation.Parameters.Count; ++i)
            {
                var parameter = operation.Parameters[i];

                var enumType = context.ApiDescription.ParameterDescriptions[i].ParameterDescriptor.ParameterType;
                if (!enumType.IsEnum)
                {
                    continue;
                }

                var schema = context.SchemaRepository.Schemas.GetOrAdd($"#/definitions/{enumType.Name}", () =>
                    context.SchemaGenerator.GenerateSchema(enumType, context.SchemaRepository)
                );

                parameter.Schema = schema;
            }
        }
    }
}