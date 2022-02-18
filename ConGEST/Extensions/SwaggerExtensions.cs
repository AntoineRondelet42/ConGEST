using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConGEST.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(
            this IServiceCollection services)
        {
            services
                .AddSwaggerGen(setupAction =>
                {
                    setupAction.SwaggerDoc("ConGEST.APIOpenAPISpecification", new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "ConGEST",
                        Version = "0.1"
                    });
                    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                    setupAction.IncludeXmlComments(xmlCommentsFullPath);
                });
            return services;
        }

        public static IApplicationBuilder UseSwaggerAsHome(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ConGEST.APIOpenAPISpecification/swagger.json", "La gestion de congés simplifiée");
                setupAction.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
