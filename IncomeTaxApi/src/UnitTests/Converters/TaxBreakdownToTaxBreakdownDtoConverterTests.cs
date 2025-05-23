using FluentAssertions;
using IncomeTaxApi.Api.Converters;
using IncomeTaxApi.Data.Models;
using IncomeTaxApi.UnitTests.TestUtilities;
using Moq;
using NUnit.Framework;

namespace IncomeTaxApi.UnitTests.Converters
{
    [TestFixture]
    public class TaxBreakdownToTaxBreakdownDtoConverterTests : Mocker<TaxBreakdownToTaxBreakdownDtoConverter>
    {
        [Test]
        public void Convert_ReturnsCorrectDto()
        {
            // Arrange
            var mockTaxBreakdown = new Mock<ITaxBreakdown>();
            mockTaxBreakdown.SetupGet(x => x.AnnualTaxPaid).Returns(4500m);
            mockTaxBreakdown.SetupGet(x => x.GrossAnnualSalary).Returns(25000m);
            mockTaxBreakdown.SetupGet(x => x.MonthlyTaxPaid).Returns(375m);
            mockTaxBreakdown.SetupGet(x => x.NetAnnualSalary).Returns(20500m);
            mockTaxBreakdown.SetupGet(x => x.NetMonthlySalary).Returns(1708.33m);
            mockTaxBreakdown.SetupGet(x => x.GrossMonthlySalary).Returns(2083.33m);

            // Act
            var result = Subject.Convert(mockTaxBreakdown.Object);

            // Assert
            result.AnnualTaxPaid.Should().Be(4500m);
            result.GrossAnnualSalary.Should().Be(25000m);
            result.MonthlyTaxPaid.Should().Be(375m);
            result.NetAnnualSalary.Should().Be(20500m);
            result.NetMonthlySalary.Should().Be(1708.33m);
            result.GrossMonthlySalary.Should().Be(2083.33m);
        }
    }
}