using Microsoft.Extensions.DependencyInjection;

namespace WBH.Livescoring.IoC
{
    public interface IModule
    {
        void RegisterServices(IServiceCollection container);
    }
}