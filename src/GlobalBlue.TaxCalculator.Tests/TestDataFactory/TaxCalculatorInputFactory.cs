
using GlobalBlue.TaxCalculator.Application.Input;

namespace GlobalBlue.TaxCalculator.Tests.TestDataFactory
{
    public static class TaxCalculatorInputFactory
    {
        public static TaxCalculatorInputDto CreateValidInput()
        {
            return new TaxCalculatorInputDto
            {
               calculationValueType = Domain.Enums.CalculationValueType.NetPrice,
               regionCode = Domain.Enums.RegionCode.Austria,
               value = 10,
               VATRate = 10 
            };
        }

        public static TaxCalculatorInputDto CreateInvalidInput_ValueLessThanZero()
        {
            return new TaxCalculatorInputDto
            {
                calculationValueType = Domain.Enums.CalculationValueType.NetPrice,
                regionCode = Domain.Enums.RegionCode.Austria,
                value = -10,
                VATRate = 10
            };
        }

        public static TaxCalculatorInputDto CreateInvalidInput_VATRateNotInThisRegion()
        {
            return new TaxCalculatorInputDto
            {
                calculationValueType = Domain.Enums.CalculationValueType.NetPrice,
                regionCode = Domain.Enums.RegionCode.Austria,
                value = 100,
                VATRate = 5
            };
        }

        public static TaxCalculatorInputDto CreateValidInputForTypeNetPrice()
        {
            return new TaxCalculatorInputDto
            {
                calculationValueType = Domain.Enums.CalculationValueType.NetPrice,
                regionCode = Domain.Enums.RegionCode.Austria,
                value = 100,
                VATRate = 10
            };
        }

        public static TaxCalculatorInputDto CreateValidInputForTypeValueAddedTax()
        {
            return new TaxCalculatorInputDto
            {
                calculationValueType = Domain.Enums.CalculationValueType.Tax,
                regionCode = Domain.Enums.RegionCode.Austria,
                value = 100,
                VATRate = 10
            };
        }

        public static TaxCalculatorInputDto CreateValidInputForTypeGrossPrice()
        {
            return new TaxCalculatorInputDto
            {
                calculationValueType = Domain.Enums.CalculationValueType.GrossPrice,
                regionCode = Domain.Enums.RegionCode.Austria,
                value = 100,
                VATRate = 10
            };
        }
    }
}
