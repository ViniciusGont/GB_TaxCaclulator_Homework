
using GlobalBlue.TaxCalculator.Application.Validations;
using GlobalBlue.TaxCalculator.Domain.Enums;
using Notifications;

namespace GlobalBlue.TaxCalculator.Application.Input
{
    public class TaxCalculatorInputDto : Notifiable
    {
        public RegionCode regionCode { get; set; } = default!;
        public double VATRate { get; set; }
        public decimal value { get; set; }
        public CalculationValueType calculationValueType { get; set; } = default!;
        public override bool IsValid() => Validate(new TaxCalculatorValuesValidation());
    }
}
