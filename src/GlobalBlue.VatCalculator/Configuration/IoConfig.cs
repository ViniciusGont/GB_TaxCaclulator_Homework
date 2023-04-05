using Notifications;
using GlobalBlue.TaxCalculator.Application.Configuration;
using GlobalBlue.TaxCalculator.Domain.Configuration;
using GlobalBlue.TaxCalculator.Application.Configuration.Mapping;

namespace GlobalBlue.TaxCalculator.Configuration
{
    public static class IoConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.ResolveApplicationIoC();
            services.ResolveDomainIoC();

            services.AddAutoMapper(typeof(ApplicationAutoMapper));
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
