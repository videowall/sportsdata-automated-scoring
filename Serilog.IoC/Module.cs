using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Extensions.Logging;
using WBH.Livescoring.IoC;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WBH.Livescoring.Serilog.IoC;

public sealed class Module : IModule
{
    #region IModule

    // Copied from Serilog GitHub
    public void RegisterServices(IServiceCollection container)
    {
        // Register Logger Factory
        container.AddSingleton<ILoggerFactory>(_ => new SerilogLoggerFactory());

        // Registered to provide two services...
        var diagnosticContext = new DiagnosticContext(null);

        // Consumed by e.g. middleware
        container.AddSingleton(diagnosticContext);

        // Consumed by user code
        container.AddSingleton<IDiagnosticContext>(diagnosticContext);

        // Install Logging
        container.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

        // Register Logger Factory
        container.AddTransient<Func<object, ILogger>>(s =>
            obj => s.GetService<ILoggerFactory>()?.CreateLogger(obj.GetType().Name)!);
    }

    #endregion
}