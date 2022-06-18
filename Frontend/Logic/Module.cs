using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WBH.Livescoring.IoC;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic;

public sealed class Module : IModule
{
    #region IModule

    public void RegisterServices(IServiceCollection container)
    {
        container.RegisterTransientAllTypesOf<Profile, Module>();
        container.RegisterTransientAllTypesOf<ILiveScoutEventHandler, Module>();
    }

    #endregion
}