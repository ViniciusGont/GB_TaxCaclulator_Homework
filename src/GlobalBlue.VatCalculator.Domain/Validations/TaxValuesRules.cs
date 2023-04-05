
using GlobalBlue.TaxCalculator.Domain.Enums;

namespace GlobalBlue.TaxCalculator.Domain.Validations
{
    public static class TaxValuesRules
    {
        public static readonly Dictionary<RegionCode, List<double>> taxesRulesByRegions = new Dictionary<RegionCode, List<double>>() {

            { RegionCode.Austria, SetAustriaVATRatesList() }

        };

        public static List<double> SetAustriaVATRatesList()
        {
            return new List<double>() { 10.0, 13.0, 20.0 };
        }
    };


}
