
using GlobalBlue.TaxCalculator.Application.Input;
using GlobalBlue.TaxCalculator.Application.Output;
using Notifications;
using AutoMapper;
using GlobalBlue.TaxCalculator.Domain;
using GlobalBlue.TaxCalculator.Domain.Taxes;
using GlobalBlue.TaxCalculator.Domain.Resources;

namespace GlobalBlue.TaxCalculator.Application.Services
{
    public class TaxCalculatorAppService : ITaxCalculatorAppService
    {
        private readonly INotifier _notifier;
        private readonly IMapper _mapper;
        private readonly IEnumerable<ITaxesCalculator> _typesOfCalculation = default!;

        public TaxCalculatorAppService(INotifier notifier, IMapper mapper, IEnumerable<ITaxesCalculator> typesOfCalculation)
        {
            _notifier = notifier;
            _mapper = mapper;
            _typesOfCalculation = typesOfCalculation;
        }

        public async Task<TaxCalculatorOutputDto?> GetTaxesValues(TaxCalculatorInputDto taxCalculatorInputDto, CancellationToken cancellation = default)
        {
            if (!taxCalculatorInputDto.IsValid())
            {
                _notifier.AddNotifications(taxCalculatorInputDto.GetNotifications());
                return null;
            }

            var calculationValues = _mapper.Map<TaxCalculatorValues>(taxCalculatorInputDto);

            var typeOfCaculation = _typesOfCalculation.FirstOrDefault(s => s.calculationValueType == taxCalculatorInputDto.calculationValueType);

            if (typeOfCaculation == null)
            {
                _notifier.AddNotification(new Notification(ErrorMessages.calculationValueTypeNotFoundCode, ErrorMessages.calculationValueTypeNotFound)); 
                return null; 
            }

            var calculationResult = await typeOfCaculation.CalculateTaxesAsync(calculationValues);

            var calculationValuesOutput = _mapper.Map<TaxCalculatorOutputDto>(calculationResult);

            return calculationValuesOutput;
        }
    }
}
