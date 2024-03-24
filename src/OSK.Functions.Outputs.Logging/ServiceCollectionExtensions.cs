using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSK.Functions.Outputs.Logging.Abstractions;
using OSK.Functions.Outputs.Logging.Internal.Services;

namespace OSK.Functions.Outputs.Logging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoggingFunctionOutputs(this IServiceCollection services)
        {
            services.AddFunctionOutputs();
            services.TryAddTransient(typeof(IOutputFactory<>), typeof(OutputFactory<>));

            return services;
        }
    }
}
