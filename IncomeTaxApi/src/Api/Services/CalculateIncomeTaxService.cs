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
            // If an update was made to the database then _unitOfWork.CompleteAsync would be required in order to 
            // finalize the transaction, however, since currently we're only retrieving the database
            // this function is not necessary
            var taxBands = (await _unitOfWork.GetRepository<ITaxBandRepository>().GetAllAsync()).ToList();

            if (!taxBands.Any())
            {
                _logger.LogError("Could not find any tax bands in the repository");
                throw new InvalidOperationException("Could not find any tax bands in the repository");
            }

            var annualTaxDeduction = 0m;
            var remainingIncome = annualIncome;
            
            // Looping through all the tax bands, and then sorting them by upper and lower limit
            // which ensures that if any future tax bands were added or modified
            // they would be calculated correctly
            foreach (var band in taxBands.OrderBy(b => b.LowerLimit))
            {
                if (annualIncome <= band.LowerLimit)
                    break;

                var upperLimit = band.UpperLimit ?? annualIncome; // Null = no upper limit (top band)
                var taxableAmount = Math.Min(upperLimit - band.LowerLimit, remainingIncome);

                if (taxableAmount <= 0)
                {
                    continue;
                }

                var rate = band.Rate / 100.0m;
                annualTaxDeduction += taxableAmount * rate;
                remainingIncome -= taxableAmount;
            }

            var monthlySalary = Math.Round(annualIncome / 12, 2);
            var monthlyTaxDeduction = Math.Round(annualTaxDeduction / 12, 2);

            // Returning a tax breakdown object which is then converted to a DTO in the command handler
            var breakdown = new TaxBreakdown
            {
                GrossAnnualSalary = annualIncome,
                GrossMonthlySalary = monthlySalary,
                AnnualTaxPaid = Math.Round(annualTaxDeduction, 2),
                MonthlyTaxPaid = monthlyTaxDeduction,
                NetAnnualSalary = Math.Round(annualIncome - annualTaxDeduction, 2),
                NetMonthlySalary = Math.Round(monthlySalary - monthlyTaxDeduction, 2)
            };
            
            _logger.LogDebug("Income tax calculated successfully!");

            return breakdown;
        }
    }
}