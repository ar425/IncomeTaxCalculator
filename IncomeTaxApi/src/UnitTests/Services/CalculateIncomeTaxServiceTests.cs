using FluentAssertions;
using IncomeTaxApi.Api.Services;
using IncomeTaxApi.Data;
using IncomeTaxApi.Data.Models;
using IncomeTaxApi.Data.Repositories;
using IncomeTaxApi.UnitTests.TestUtilities;
using Moq;
using NUnit.Framework;

namespace IncomeTaxApi.UnitTests.Services
{

    [TestFixture]
    public class CalculateIncomeTaxServiceTests : Mocker<CalculateIncomeTaxService>
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = null!;
        private Mock<ITaxBandRepository> _taxBandRepoMock = null!;

        protected override void SetUp()
        {
            _unitOfWorkMock = GetMock<IUnitOfWork>();
            _taxBandRepoMock = new Mock<ITaxBandRepository>();

            // Setup the unitOfWork to return the mocked taxBandRepo
            _unitOfWorkMock
                .Setup(uow => uow.GetRepository<ITaxBandRepository>())
                .Returns(_taxBandRepoMock.Object);
        }

        // For multiple tests using different values we can use [TestCase()]
        // however in this instance I feel that there would be too many variables involved
        // and would quickly clutter the code
        [Test]
        public async Task CalculateIncomeTax_ReturnsCorrectValue()
        {
            // Arrange

            var taxBand1 = new TaxBand { LowerLimit = 0, UpperLimit = 10000, Rate = 10 };
            var taxBand2 = new TaxBand { LowerLimit = 10000, UpperLimit = 20000, Rate = 20 };
            var taxBand3 = new TaxBand { LowerLimit = 20000, UpperLimit = null, Rate = 30 };

            var taxBandList = new List<TaxBand>() { taxBand1, taxBand2, taxBand3 };

             _taxBandRepoMock
                 .Setup(repo => repo.GetAllAsync())!
                 .ReturnsAsync(taxBandList);

            var annualIncome = 25000m;
            var annualIncomeResult = 4500m;

            // Act
            var result = await Subject.CalculateIncomeTaxAsync(annualIncome);

            // Assert

            result.GrossAnnualSalary.Should().Be(annualIncome);
            result.AnnualTaxPaid.Should().Be(annualIncomeResult);
            result.MonthlyTaxPaid.Should().Be(Math.Round(annualIncomeResult / 12, 2));
            result.NetAnnualSalary.Should().Be(annualIncome - annualIncomeResult);
            result.NetMonthlySalary.Should().Be(Math.Round((annualIncome / 12) - (annualIncomeResult / 12), 2));
        }

        [Test]
        public async Task CalculateIncomeTax_ThrowsException_WhenNoTaxBands()
        {
            // Arrange
            _taxBandRepoMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync([]);

            // Act
            var result = async () => await Subject.CalculateIncomeTaxAsync(10000);
            
            // Assert
            await result.Should().ThrowAsync<Exception>().WithMessage("Could not find any tax bands in the repository");
        }
    }
}