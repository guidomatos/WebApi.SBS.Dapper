using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.SBS.Core.Filter
{
    public class RemoveVersionParameterFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "version");
            if (versionParameter == default(OpenApiParameter)) return;
            var parameters = operation.Parameters.ToList();
            operation.Parameters.Remove(versionParameter);
        }
    }
}
