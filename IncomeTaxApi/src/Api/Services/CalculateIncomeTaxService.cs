using IncomeTaxApi.Data;
using IncomeTaxApi.Data.Models;
using IncomeTaxApi.Data.Repositories;

namespace IncomeTaxApi.Api.Services
{
    // Using interfaces so that the code depends on abstractions rather than concrete implementations
    // containing only method signatures, properties, or events
    // Also helps with testability, reusability, and clean architecture
    public interface ICalculateIncomeTaxService
    {
        Task<ITaxBreakdown> CalculateIncomeTaxAsync(decimal annualIncome);
    }

    public class CalculateIncomeTaxService: ICalculateIncomeTaxService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CalculateIncomeTaxService> _logger;

        public CalculateIncomeTaxService(IUnitOfWork unitOfWork, ILogger<CalculateIncomeTaxService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ITaxBreakdown> CalculateIncomeTaxAsync(decimal annualIncome)
        {
            var taxBands = (await _unitOfWork.GetRepository<ITaxBandRepository>().GetAllAsync()).ToList();

            if (taxBands.Count() < 0)
            {
                _logger.LogError("Could not find any tax bands in the repository");
                throw new Exception("Could not find any tax bands in the repository");
            }
            
            decimal annualTaxDeduction = 0m;
            decimal remainingIncome = annualIncome;
            
            foreach (var band in taxBands.OrderBy(b => b.LowerLimit))
            {
                if (annualIncome <= band.LowerLimit)
                    break;

                var upperLimit = band.UpperLimit ?? annualIncome; // Null = no upper limit (top band)
                var taxableAmount = Math.Min(upperLimit - band.LowerLimit, remainingIncome);

                if (taxableAmount > 0)
                {
                    var rate = band.Rate / 100.0m;
                    annualTaxDeduction += taxableAmount * rate;
                    remainingIncome -= taxableAmount;
                }
            }

            var monthlySalary = Math.Round(annualIncome / 12, 2);
            var monthlyTaxDeduction = Math.Round(annualTaxDeduction / 12, 2);

            var breakdown = new TaxBreakdown
            {
                GrossAnnualSalary = annualIncome,
                GrossMonthlySalary = monthlySalary,
                AnnualTaxPaid = Math.Round(annualTaxDeduction, 2),
                MonthlyTaxPaid = monthlyTaxDeduction,
                NetAnnualSalary = Math.Round(annualIncome - annualTaxDeduction, 2),
                NetMonthlySalary = Math.Round(monthlySalary - monthlyTaxDeduction, 2)
            };

            return breakdown;
        }
    }
}