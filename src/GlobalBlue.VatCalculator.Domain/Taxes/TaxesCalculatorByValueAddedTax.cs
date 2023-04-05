
using GlobalBlue.TaxCalculator.Domain.Enums;

namespace GlobalBlue.TaxCalculator.Domain.Taxes
{
    public class TaxesCalculatorByValueAddedTax : ITaxesCalculator
    {
        public CalculationValueType calculationValueType => CalculationValueType.Tax;
        public async Task<TaxCalculatorValuesResult> CalculateTaxesAsync(TaxCalculatorValues taxCalculatorValues)
        {
            var VATRatePerc = taxCalculatorValues.VATRate / 100;

            var grossPrice = Math.Round(taxCalculatorValues.value * (1 + VATRatePerc) / VATRatePerc, 2);
            var netPrice = Math.Round(taxCalculatorValues.value / VATRatePerc, 2);

            var result = new TaxCalculatorValuesResult(taxCalculatorValues.value, netPrice, grossPrice);

            return await Task.FromResult(result);
        }
    }
}
