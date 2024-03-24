using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;

namespace OSK.Functions.Outputs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFunctionOutputs(this IServiceCollection services)
        {
            services.TryAddTransient<IOutputFactory, OutputFactory>();

            return services;
        }
    }
}
