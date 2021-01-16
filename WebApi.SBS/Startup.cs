using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebApi.SBS.ApplicationCore.Interfaces.Repositories;
using WebApi.SBS.ApplicationCore.Interfaces.Services;
using WebApi.SBS.ApplicationCore.Services;
using WebApi.SBS.Core.Filter;
using WebApi.SBS.Core.Swagger;
using WebApi.SBS.Infrastructure.Repositories;

namespace WebApi.SBS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region "Api Versioning"
            // Add API Versioning to the Project
            services.AddApiVersioning(apiVersioningOptions =>
            {
                // Specify the default API Version as 1.0
                apiVersioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number
                apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                apiVersioningOptions.ReportApiVersions = true;

                //apiVersioningOptions.ApiVersionReader = new MediaTypeApiVersionReader("v");
                apiVersioningOptions.ApiVersionReader = new UrlSegmentApiVersionReader();

                //HTTP Header based versioning
                //apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader("x-api-version");

                //apiVersioningOptions.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));

            });
            #endregion


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                var swaggerOptions = new SwaggerConfiguration();
                Configuration.GetSection("Swagger").Bind(swaggerOptions);

                foreach (var currentVersion in swaggerOptions.Versions)
                {
                    swaggerGenOptions.SwaggerDoc(currentVersion.Name, new OpenApiInfo
                    {
                        Title = swaggerOptions.Title,
                        Version = currentVersion.Name,
                        Description = swaggerOptions.Description,
                        /*
                        TermsOfService = new Uri("http://tempuri.org/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Guido Matos Camones",
                            Email = "guido.matos.88@gmail.com"
                        },
                        */
                        License = new OpenApiLicense
                        {
                            Name = "Superintendencia de Banca, Seguros y AFP - Website",
                            Url = new Uri("https://www.sbs.gob.pe/")
                        }
                    });

                    /*
                    var filePath = Path.Combine(System.AppContext.BaseDirectory, $"ApiSBSv{currentVersion.Name}.xml");
                    swaggerGenOptions.IncludeXmlComments(filePath);
                    */
                }

                swaggerGenOptions.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
                    {
                        return false;
                    }
                    var versions = methodInfo.DeclaringType.GetConstructors()
                        .SelectMany(constructorInfo => constructorInfo.DeclaringType.CustomAttributes
                            .Where(attributeData => attributeData.AttributeType == typeof(ApiVersionAttribute))
                            .SelectMany(attributeData => attributeData.ConstructorArguments
                                .Select(attributeTypedArgument => attributeTypedArgument.Value)));

                    return versions.Any(v => $"{v}" == version);
                });

                swaggerGenOptions.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                swaggerGenOptions.OperationFilter<RemoveVersionParameterFilter>();
                swaggerGenOptions.EnableAnnotations();

            });


            //Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //Service
            services.AddScoped<IUsuarioService, UsuarioService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            var swaggerOptions = new SwaggerConfiguration();
            Configuration.GetSection("Swagger").Bind(swaggerOptions);
            app.UseSwagger(option => option.RouteTemplate = swaggerOptions.JsonRoute);

            app.UseSwaggerUI(option =>
            {
                foreach (var currentVersion in swaggerOptions.Versions)
                {
                    option.SwaggerEndpoint(currentVersion.UiEndpoint, $"{swaggerOptions.Title} v{currentVersion.Name}");
                    //option.RoutePrefix = string.Empty;
                    option.DocumentTitle = swaggerOptions.Title;
                    option.DocExpansion(DocExpansion.List);
                }
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("The service is available.");
            });
        }
    }
}
