
using GlobalBlue.TaxCalculator.Domain.Taxes;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalBlue.TaxCalculator.Domain.Configuration
{
    public static class IoC
    {
        public static IServiceCollection ResolveDomainIoC(this IServiceCollection services)
        {
            services.AddTransient<ITaxesCalculator, TaxesCalculatorByGrossPrice>();
            services.AddTransient<ITaxesCalculator, TaxesCalculatorByNetPrice>();
            services.AddTransient<ITaxesCalculator, TaxesCalculatorByValueAddedTax>();

            return services;
        }
    }
}
