namespace GlobalBlue.TaxCalculator.Domain
{
    public class TaxCalculatorValuesResult
    {
        public decimal valueAddedTax { get; set; }
        public decimal netPrice { get; set; }
        public decimal grossPrice { get; set; }

        public TaxCalculatorValuesResult(decimal valueAddedTax, decimal netPrice, decimal grossPrice)
        {
            this.valueAddedTax = valueAddedTax;
            this.netPrice = netPrice;
            this.grossPrice = grossPrice;
        }
    }
}