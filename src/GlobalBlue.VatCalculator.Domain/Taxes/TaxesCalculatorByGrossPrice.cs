
using GlobalBlue.TaxCalculator.Domain.Enums;

namespace GlobalBlue.TaxCalculator.Domain.Taxes
{
    public class TaxesCalculatorByGrossPrice : ITaxesCalculator
    {
        public CalculationValueType calculationValueType => CalculationValueType.GrossPrice;

        public async Task<TaxCalculatorValuesResult> CalculateTaxesAsync(TaxCalculatorValues taxCalculatorValues)
        {
            var VATRatePerc = taxCalculatorValues.VATRate / 100;

            var netPrice = Math.Round(taxCalculatorValues.value / (1 + VATRatePerc), 2);
            var valueAddedTax = Math.Round(taxCalculatorValues.value * VATRatePerc / (1 + VATRatePerc), 2);

            var result = new TaxCalculatorValuesResult(valueAddedTax, netPrice, taxCalculatorValues.value);

            return await Task.FromResult(result);
        }
    }
}
