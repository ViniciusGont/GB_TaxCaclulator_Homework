using GlobalBlue.TaxCalculator.Application.Input;
using GlobalBlue.TaxCalculator.Application.Output;

namespace GlobalBlue.TaxCalculator.Application.Services
{
    public interface ITaxCalculatorAppService
    {
        Task<TaxCalculatorOutputDto?> GetTaxesValues(TaxCalculatorInputDto taxCalculatorInputDto, CancellationToken cancellation = default);
    }
}