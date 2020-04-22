using System;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dukkan.Web.Swagger
{
    public class SwaggerEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (!type.IsEnum || schema.Extensions.ContainsKey("x-enumNames"))
            {
                return;
            }

            var enumNames = new OpenApiArray();
            enumNames.AddRange(Enum.GetNames(type).Select(_ => new OpenApiString(_)));
            schema.Extensions.Add("x-enumNames", enumNames);
        }
    }
}