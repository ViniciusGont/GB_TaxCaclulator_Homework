
using GlobalBlue.TaxCalculator.Domain.Enums;

namespace GlobalBlue.TaxCalculator.Domain.Taxes
{
    public interface ITaxesCalculator
    { 
        Task<TaxCalculatorValuesResult> CalculateTaxesAsync(TaxCalculatorValues taxCalculatorValues);

        CalculationValueType calculationValueType { get; }
    }
}
