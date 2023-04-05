using AutoMapper;
using GlobalBlue.TaxCalculator.Application.Input;
using GlobalBlue.TaxCalculator.Application.Output;
using GlobalBlue.TaxCalculator.Domain;

namespace GlobalBlue.TaxCalculator.Application.Configuration.Mapping
{
    public class ApplicationAutoMapper : Profile
    {
        public ApplicationAutoMapper() 
        {
            CreateMap<TaxCalculatorInputDto, TaxCalculatorValues>();
            CreateMap<TaxCalculatorValuesResult, TaxCalculatorOutputDto>(); 
        }  
    }
}
