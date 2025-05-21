namespace IncomeTaxApi.Data.Models
{
    public interface ITaxBreakdown
    {
        decimal GrossAnnualSalary { get; set; }
        decimal GrossMonthlySalary { get; set; }
        decimal NetAnnualSalary { get; set; }
        decimal NetMonthlySalary { get; set; }
        decimal AnnualTaxPaid { get; set; }
        decimal MonthlyTaxPaid { get; set; }
        
    }
    
    public class TaxBreakdown : ITaxBreakdown
    {
        public decimal GrossAnnualSalary { get; set; }
        public decimal GrossMonthlySalary { get; set; }
        public decimal NetAnnualSalary { get; set; }
        public decimal NetMonthlySalary { get; set; }
        public decimal AnnualTaxPaid { get; set; }
        public decimal MonthlyTaxPaid { get; set; }
    }
}