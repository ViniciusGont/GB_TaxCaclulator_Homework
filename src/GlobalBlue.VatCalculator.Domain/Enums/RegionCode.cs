using System.ComponentModel;

namespace GlobalBlue.TaxCalculator.Domain.Enums
{
    public enum RegionCode
    {
        //If not send via json, the application will use Austria as default
        [Description("Austria")]
        Austria = 0
        
        //Add others coutries or regions if you need. 

    }
}
