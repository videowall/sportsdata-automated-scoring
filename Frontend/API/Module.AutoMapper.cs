using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace WBH.Livescoring.Frontend.API;

public sealed partial class Module
{
    #region Methods

    private static void RegisterAutoMapper(IServiceCollection container)
    {
        container.AddAutoMapper((s, c) => c.AddProfiles(s.GetServices<Profile>()), Array.Empty<Type>());
    }

    #endregion
}