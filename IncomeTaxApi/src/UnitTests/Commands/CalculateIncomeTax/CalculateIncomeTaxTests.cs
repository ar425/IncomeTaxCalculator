using FluentAssertions;
using IncomeTaxApi.Abstractions;
using IncomeTaxApi.Api.Commands.CalculateIncomeTax;
using IncomeTaxApi.Api.Dtos;
using IncomeTaxApi.Api.Services;
using IncomeTaxApi.Data.Models;
using IncomeTaxApi.UnitTests.TestUtilities;
using Moq;
using NUnit.Framework;

namespace IncomeTaxApi.UnitTests.Commands.CalculateIncomeTax
{

    [TestFixture]
    public class CalculateIncomeTaxTests : Mocker<CalculateIncomeTaxCommandHandler>
    {
        private Mock<ICalculateIncomeTaxService> _taxServiceMock = null!;
        private Mock<IConverter<ITaxBreakdown, TaxBreakdownDto>> _converterMock = null!;

        [SetUp]
        protected override void SetUp()
        {
            _taxServiceMock = GetMock<ICalculateIncomeTaxService>();
            _converterMock = GetMock<IConverter<ITaxBreakdown, TaxBreakdownDto>>();
        }

        [Test]
        public async Task HandleAsync_ValidCommand_ReturnsConvertedDto()
        {
            // Arrange
            var command = new CalculateIncomeTaxCommand { AnnualSalaryAmount = 30000 };

            var taxBreakdown = new Mock<ITaxBreakdown>().Object;
            var taxBreakdownDto = new TaxBreakdownDto { AnnualTaxPaid = 5000m };

            _taxServiceMock
                .Setup(s => s.CalculateIncomeTaxAsync(command.AnnualSalaryAmount))
                .ReturnsAsync(taxBreakdown);

            _converterMock
                .Setup(c => c.Convert(taxBreakdown))
                .Returns(taxBreakdownDto);

            // Act
            var result = await Subject.HandleAsync(command);

            // Assert
            result.Should().Be(taxBreakdownDto);
            _taxServiceMock.Verify(s => s.CalculateIncomeTaxAsync(command.AnnualSalaryAmount), Times.Once);
            _converterMock.Verify(c => c.Convert(taxBreakdown), Times.Once);
        }

        [Test]
        public void HandleAsync_WhenServiceThrows_LogsAndThrowsGenericException()
        {
            // Arrange
            var command = new CalculateIncomeTaxCommand { AnnualSalaryAmount = 30000 };
            var exception = new InvalidOperationException("Something went wrong");

            _taxServiceMock
                .Setup(s => s.CalculateIncomeTaxAsync(It.IsAny<decimal>()))
                .ThrowsAsync(exception);

            // Act
            var result = async () => await Subject.HandleAsync(command);
            
            // Assert
            result.Should().ThrowAsync<Exception>().WithMessage("Unexpected exception thrown when attempting to calculate income tax");
        }
    }
}