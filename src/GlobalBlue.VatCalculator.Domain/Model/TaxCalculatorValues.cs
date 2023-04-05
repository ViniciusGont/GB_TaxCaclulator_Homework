using GlobalBlue.TaxCalculator.Domain.Enums;

namespace GlobalBlue.TaxCalculator.Domain
{
    public class TaxCalculatorValues
    {
        public decimal VATRate { get; set; }
        public decimal value { get; set; }
        public RegionCode regionCode { get; set; }
        public CalculationValueType calculationValueType { get; set; }

    }
}