using FluentValidation;
using GlobalBlue.TaxCalculator.Application.Input;
using GlobalBlue.TaxCalculator.Domain.Resources;
using GlobalBlue.TaxCalculator.Domain.Validations;

namespace GlobalBlue.TaxCalculator.Application.Validations
{
    public class TaxCalculatorValuesValidation : AbstractValidator<TaxCalculatorInputDto>
    {
        public TaxCalculatorValuesValidation()
        {
            RuleFor(tax => tax.value).NotEmpty().WithMessage(string.Format(ErrorMessages.EmptyValue, "{PropertyName}"))
                                 .GreaterThan(0).WithMessage(string.Format(ErrorMessages.LessThanZeroValue, "{PropertyName}"));

            RuleFor(tax => tax.VATRate).NotEmpty().WithMessage(string.Format(ErrorMessages.EmptyValue, "{PropertyName}"))
                                   .GreaterThan(0).WithMessage(string.Format(ErrorMessages.LessThanZeroValue, "{PropertyName}"));

            RuleFor(tax => tax.regionCode).NotNull().WithMessage(string.Format(ErrorMessages.EmptyValue, "{PropertyName}"))
                                          .IsInEnum().WithMessage(string.Format(ErrorMessages.EmptyValue, "{PropertyName}"));

            RuleFor(tax => tax.calculationValueType).NotNull().WithMessage(string.Format(ErrorMessages.EmptyValue, "{PropertyName}"))
                                               .IsInEnum().WithMessage(string.Format(ErrorMessages.EmptyValue, "{PropertyName}"));

            RuleFor(tax => tax).Must(TaxesRulesByCountryValidation).WithName(ErrorMessages.RegionVATNotFound).WithMessage(ErrorMessages.RateNotIncludedInRegion);

            //Add more if needed
        }

        private bool TaxesRulesByCountryValidation(TaxCalculatorInputDto arg)
        {
            if (TaxValuesRules.taxesRulesByRegions.ContainsKey(arg.regionCode))
                return TaxValuesRules.taxesRulesByRegions[arg.regionCode].Contains(arg.VATRate);

            return false;
        }
    }
}
