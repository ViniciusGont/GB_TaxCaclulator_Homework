using GlobalBlue.TaxCalculator.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalBlue.TaxCalculator.Application.Configuration
{
    public static class IoC
    {
        public static IServiceCollection ResolveApplicationIoC(this IServiceCollection services)
        {
            services.AddScoped<ITaxCalculatorAppService, TaxCalculatorAppService>();

            return services;
        }
    }
}
