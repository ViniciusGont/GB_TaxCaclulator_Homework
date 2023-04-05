using GlobalBlue.TaxCalculator.Application.Input;
using GlobalBlue.TaxCalculator.Application.Output;
using GlobalBlue.TaxCalculator.Application.Services;
using GlobalBlue.TaxCalculator.Controllers.Extensions;
using Microsoft.AspNetCore.Mvc;
using Notifications;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace GlobalBlue.TaxCalculator.Controllers
{
    [Route("tax")]
    public class TaxCaculatorController : BaseController
    {
        ITaxCalculatorAppService _taxCalculatorAppService;

        public TaxCaculatorController(INotifier notifier, ITaxCalculatorAppService taxCalculatorAppService) : base(notifier)
        {
            _taxCalculatorAppService = taxCalculatorAppService;
        }

        [HttpGet("calculateTaxes")]
        [SwaggerOperation(Summary = "Caclulate taxes rates. If the API receives one of the net, gross or VAT amounts and additionally a valid\r\n" +
            "Austrian VAT rate (10%, 13%, 20%), the other two missing amounts\r\n" +
            "(net/gross/VAT) are calculated by the system and returned to the client in a\r\nmeaningful structure")]
        [ProducesResponseType(typeof(TaxCalculatorOutputDto), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status422UnprocessableEntity)]
        public async Task<IActionResult> GetTaxValues([FromQuery] TaxCalculatorInputDto taxCalculatorValues, CancellationToken cancellationToken)
        {
            var calculationResult = await _taxCalculatorAppService.GetTaxesValues(taxCalculatorValues, cancellationToken);

            if (!IsValidOperation())
                return InvalidOperationResult();

            return Ok(calculationResult);
        }
    }
}
