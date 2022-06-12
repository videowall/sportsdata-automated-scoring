using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Serilog;
using WBH.Livescoring.Frontend.API.Common;
using WBH.Livescoring.IoC;

namespace WBH.Livescoring.Frontend.API
{
    public class Startup
    {
        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        #endregion

        #region Properties

        private IConfiguration Configuration { get; }

        #endregion

        #region Methods

        public void ConfigureServices(IServiceCollection services) => services
            .AddSingleton(Configuration)
            .InstallModules();
        
        public void Configure(IApplicationBuilder applicationBuilder, IHostEnvironment webHostEnvironment)
        {
            applicationBuilder.UseForwardedHeaders();
            
            if (webHostEnvironment.IsDevelopment())
                applicationBuilder
                    .UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI();
            else
                applicationBuilder.UseHsts();

            applicationBuilder.UseHttpsRedirection()
                .UseRouting()
                .UseCors()
                //.UseAuthentication()
                //.UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();

                    endpoints.MapHealthChecks("/api/health", new HealthCheckOptions()
                    {
                        ResultStatusCodes =
                        {
                            [HealthStatus.Healthy] = StatusCodes.Status200OK,
                            [HealthStatus.Degraded] = StatusCodes.Status200OK,
                            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                        }
                    });
                })
                .UseRewriteUnknownPathsToIndexSite("/wwwroot")
                .UseStaticFiles();
        }

        #endregion
    }
}