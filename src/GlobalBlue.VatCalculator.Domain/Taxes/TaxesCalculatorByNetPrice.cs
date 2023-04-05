
using GlobalBlue.TaxCalculator.Domain.Enums;

namespace GlobalBlue.TaxCalculator.Domain.Taxes
{
    public class TaxesCalculatorByNetPrice : ITaxesCalculator
    {
        public CalculationValueType calculationValueType => CalculationValueType.NetPrice;
        public async Task<TaxCalculatorValuesResult> CalculateTaxesAsync(TaxCalculatorValues taxCalculatorValues)
        {
            var VATRatePerc = taxCalculatorValues.VATRate / 100;

            var valueAddedTax = Math.Round(taxCalculatorValues.value * VATRatePerc, 2);
            var grossPrice = Math.Round(taxCalculatorValues.value * (1 + VATRatePerc), 2);

            var result = new TaxCalculatorValuesResult(valueAddedTax, taxCalculatorValues.value, grossPrice);

            return await Task.FromResult(result);
        }
    }
}
