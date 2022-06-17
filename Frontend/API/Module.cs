using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WBH.Livescoring.IoC;

namespace WBH.Livescoring.Frontend.API
{
    public sealed partial class Module : IModule
    {
        #region IModule

        public void RegisterServices(IServiceCollection container)
        {
            container.AddTransient<Func<object, ILogger>>(s => obj => s.GetService<ILoggerFactory>()?.CreateLogger(obj.GetType().Name)!);
            RegisterMvc(container);
            RegisterHealthChecks(container);
            RegisterSwagger(container);
        }

        #endregion
    }
}