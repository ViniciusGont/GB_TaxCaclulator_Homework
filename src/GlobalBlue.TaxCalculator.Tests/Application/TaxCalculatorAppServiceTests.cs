using AutoMapper;
using FluentAssertions;
using GlobalBlue.TaxCalculator.Application.Configuration.Mapping;
using GlobalBlue.TaxCalculator.Application.Services;
using GlobalBlue.TaxCalculator.Domain.Taxes;
using GlobalBlue.TaxCalculator.Tests.TestDataFactory;
using Moq;
using Notifications;

namespace GlobalBlue.TaxCalculator.Tests.Application
{
    public class TaxCalculatorAppServiceTests
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<INotifier> _notifierMock;
        private readonly Mock<IEnumerable<ITaxesCalculator>> _typesOfCalculationMock;

        public TaxCalculatorAppServiceTests()
        {
            _notifierMock = new Mock<INotifier>();

            var calculatorMock1 = new Mock<TaxesCalculatorByNetPrice>();
            var calculatorMock2 = new Mock<TaxesCalculatorByGrossPrice>();
            var calculatorMock3 = new Mock<TaxesCalculatorByValueAddedTax>();
            IEnumerable<ITaxesCalculator> items = new List<ITaxesCalculator> { calculatorMock1.Object, calculatorMock2.Object, calculatorMock3.Object };

            _typesOfCalculationMock = new Mock<IEnumerable<ITaxesCalculator>>();
            _typesOfCalculationMock.Setup(s => s.GetEnumerator()).Returns(items.GetEnumerator());

            _mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationAutoMapper>();
            }).CreateMapper();
        }

        private TaxCalculatorAppService CreateTaxCalculatorAppService()
        {
            return new TaxCalculatorAppService(
                _notifierMock.Object,
                _mapperMock,
                _typesOfCalculationMock.Object
                );
        }


        [Fact(DisplayName = "Should not have notification when data is valid")]
        [Trait("Category", "TaxCalculation_ValidInput")]
        public void CalculateTaxes_WhenDataIsValid()
        {
            // Arrange
            var appService = CreateTaxCalculatorAppService();
            var input = TaxCalculatorInputFactory.CreateValidInput();

            //Act
            var result = appService.GetTaxesValues(input);

            //Asserts
            //Cant have notifications added called
            _notifierMock.Verify(m => m.AddNotifications(It.IsAny<IEnumerable<Notification>>()), Times.Never());
        }

        [Fact(DisplayName = "Can't calculate if value provided is less than 0")]
        [Trait("Category", "TaxCalculation_InvalidInput")]
        public void CalculateTaxes_WhenValueLessThanZero_MustHaveNotifications()
        {
            // Arrange
            var appService = CreateTaxCalculatorAppService();
            var input = TaxCalculatorInputFactory.CreateInvalidInput_ValueLessThanZero();

            //Act
            var result = appService.GetTaxesValues(input);

            //Asserts
            //Must have notifications added
            _notifierMock.Verify(m => m.AddNotifications(It.IsAny<IEnumerable<Notification>>()), Times.Once());
        }

        [Fact(DisplayName = "Can't calculate if VATrate provided doesn't exist for that region")]
        [Trait("Category", "TaxCalculation_InvalidInput")]
        public void CalculateTaxes_WhenVATRateNotInThisRegion_MustHaveNotifications()
        {
            // Arrange
            var appService = CreateTaxCalculatorAppService();
            var input = TaxCalculatorInputFactory.CreateInvalidInput_VATRateNotInThisRegion();

            //Act
            var result = appService.GetTaxesValues(input);

            //Asserts
            //Must have notifications added
            _notifierMock.Verify(m => m.AddNotifications(It.IsAny<IEnumerable<Notification>>()), Times.Once());
        }

        [Fact(DisplayName = "Caclulation result must be ok when data is valid")]
        [Trait("Category", "TaxCalculation_CalculationResult")]
        public async void CalculateTaxes_WhenDataIsValidAndTypeIsNetPrice()
        {
            // Arrange
            var appService = CreateTaxCalculatorAppService();
            var input = TaxCalculatorInputFactory.CreateValidInputForTypeNetPrice();

            //Act
            var result = await appService.GetTaxesValues(input);

            //Asserts
            _notifierMock.Verify(m => m.AddNotifications(It.IsAny<IEnumerable<Notification>>()), Times.Never());

            result.grossPrice.Should().Be(110);
            result.valueAddedTax.Should().Be(10);
        }

        [Fact(DisplayName = "Caclulation result must be ok when data is valid")]
        [Trait("Category", "TaxCalculation_CalculationResult")]
        public async void CalculateTaxes_WhenDataIsValidAndTypeIsTypeValueAddedTax()
        {
            // Arrange
            var appService = CreateTaxCalculatorAppService();
            var input = TaxCalculatorInputFactory.CreateValidInputForTypeValueAddedTax();

            //Act
            var result = await appService.GetTaxesValues(input);

            //Asserts
            _notifierMock.Verify(m => m.AddNotifications(It.IsAny<IEnumerable<Notification>>()), Times.Never());

            result.grossPrice.Should().Be(1100);
            result.netPrice.Should().Be(1000);
        }

        [Fact(DisplayName = "Caclulation result must be ok when data is valid")]
        [Trait("Category", "TaxCalculation_CalculationResult")]
        public async void CalculateTaxes_WhenDataIsValidAndTypeIsTypePriceInclVAT()
        {
            // Arrange
            var appService = CreateTaxCalculatorAppService();
            var input = TaxCalculatorInputFactory.CreateValidInputForTypeGrossPrice();

            //Act
            var result = await appService.GetTaxesValues(input);

            //Asserts
            _notifierMock.Verify(m => m.AddNotifications(It.IsAny<IEnumerable<Notification>>()), Times.Never());

            result.netPrice.Should().Be(90.91M);
            result.valueAddedTax.Should().Be(9.09M);
        }

        //Add many more if you need
    }
}