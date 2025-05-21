namespace IncomeTaxApi.Api.Commands.CalculateIncomeTax
{
    // The object passed from UI to the controller
    public class CalculateIncomeTaxCommand
    {
        public int AnnualSalaryAmount { get; set; }
    }
}