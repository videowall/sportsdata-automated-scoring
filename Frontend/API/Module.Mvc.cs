using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace WBH.Livescoring.Frontend.API;

public sealed partial class Module
{
    #region Methods

    private static void RegisterMvc(IServiceCollection container)
    {
        container.AddMvc().AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()));
    }

    #endregion
}