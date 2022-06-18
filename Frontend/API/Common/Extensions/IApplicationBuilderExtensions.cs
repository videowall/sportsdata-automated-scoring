using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;

namespace WBH.Livescoring.Frontend.API.Common;

// ReSharper disable once InconsistentNaming
public static class IApplicationBuilderExtensions
{
    #region Constants

    private const string DEFAULT_SITE = "/index.html";

    #endregion

    #region Methods

    public static IApplicationBuilder UseRewriteUnknownPathsToIndexSite(this IApplicationBuilder applicationBuilder,
        string apiBaseUrl)
    {
        return applicationBuilder.Use(async (context, next) =>
        {
            await next();

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound
                && !Path.HasExtension(context.Request.Path.Value)
                && context.Request.Path.Value?.StartsWith(apiBaseUrl) != true)
            {
                context.Request.Path = DEFAULT_SITE;
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await next();
            }
        });
    }

    #endregion
}