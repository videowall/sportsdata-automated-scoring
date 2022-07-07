using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace WBH.Livescoring.Frontend.API;

public sealed partial class Module
{
    #region Methods

    private static void RegisterSwagger(IServiceCollection container)
    {
        var assembly = Assembly.GetExecutingAssembly().GetName();
        container.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v" + assembly.Version?.Major, new OpenApiInfo
            {
                Title = $"{assembly.Name}",
                Version = assembly.Version?.ToString() ?? "1.0.0",
                Description = $"{assembly.Name} REST API"
            });

            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

            // Include XML Documentation
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var xmlPath = Path.Combine(basePath, assembly.Name + ".xml");
            c.IncludeXmlComments(xmlPath);
        });
        container.AddSwaggerGenNewtonsoftSupport();
    }

    #endregion
}