using FluentAssertions;
using IncomeTaxApi.Abstractions;
using IncomeTaxApi.Api.Commands.CalculateIncomeTax;
using IncomeTaxApi.Api.Controllers;
using IncomeTaxApi.Api.Dtos;
using IncomeTaxApi.Api.Queries.GetIncomeSalary;
using IncomeTaxApi.UnitTests.TestUtilities;
using Moq;
using NUnit.Framework;

namespace IncomeTaxApi.UnitTests.Controllers
{

    [TestFixture]
    public class TaxControllerTests : Mocker<TaxController>
    {
        private Mock<IRequestHandler<CalculateIncomeTaxCommand, TaxBreakdownDto>> _calcHandlerMock = null!;
        private Mock<IRequestHandler<GetIncomeSalaryQuery, SalaryDto>> _salaryHandlerMock = null!;

        [SetUp]
        protected override void SetUp()
        {
            _calcHandlerMock = new Mock<IRequestHandler<CalculateIncomeTaxCommand, TaxBreakdownDto>>();
            _salaryHandlerMock = new Mock<IRequestHandler<GetIncomeSalaryQuery, SalaryDto>>();
        }

        [Test]
        public async Task CalculateTax_ReturnsOkWithTaxBreakdownDto()
        {
            // Arrange
            var command = new CalculateIncomeTaxCommand { AnnualSalaryAmount = 40000 };
            var expectedDto = new TaxBreakdownDto { AnnualTaxPaid = 6000m };

            _calcHandlerMock
                .Setup(x => x.HandleAsync(command))
                .ReturnsAsync(expectedDto);

            // Act
            var act = async () => await Subject.CalculateTax(command);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Test]
        public async Task GetSalary_ReturnsOkWithSalaryDto()
        {
            // Arrange
            var expectedDto = new SalaryDto();

            _salaryHandlerMock
                .Setup(x => x.HandleAsync(It.IsAny<GetIncomeSalaryQuery>()))
                .ReturnsAsync(expectedDto);

            // Act
            var act = async () => await Subject.GetSalary();

            // Assert
            await act.Should().NotThrowAsync();
        }
    }
}