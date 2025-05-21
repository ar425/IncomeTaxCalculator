using IncomeTaxApi.Abstractions;
using IncomeTaxApi.Api.Dtos;
using IncomeTaxApi.Data.Models;

namespace IncomeTaxApi.Api.Converters;

public class TaxBreakdownToTaxBreakdownDtoConverter : IConverter<ITaxBreakdown, TaxBreakdownDto>
{
    // Converter to convert the entity passed from the database to an object that will then
    // be used in the controller
    // Doing it this way reduces duplication if this mapping logic is needed in more than one place
    // Adds clear boundaries between layers as well as improves testability
    public TaxBreakdownDto Convert(ITaxBreakdown taxBreakdown)
    {
        return new TaxBreakdownDto
        {
            AnnualTaxPaid = taxBreakdown.AnnualTaxPaid,
            GrossAnnualSalary = taxBreakdown.GrossAnnualSalary,
            MonthlyTaxPaid = taxBreakdown.MonthlyTaxPaid,
            NetAnnualSalary = taxBreakdown.NetAnnualSalary,
            NetMonthlySalary = taxBreakdown.NetMonthlySalary,
            GrossMonthlySalary = taxBreakdown.GrossMonthlySalary
        };
    }
}