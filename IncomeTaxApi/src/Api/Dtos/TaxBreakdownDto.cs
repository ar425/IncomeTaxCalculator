using System.Text.Json.Serialization;
using IncomeTaxApi.Api.Converters;

namespace IncomeTaxApi.Api.Dtos
{
    public class TaxBreakdownDto
    {
        // Added a custom json converter to ensure that these values are restricted to
        // 2 decimal places as they are monetary values and should never exceed that
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal GrossAnnualSalary { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal GrossMonthlySalary { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal NetAnnualSalary { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal NetMonthlySalary { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal AnnualTaxPaid { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal MonthlyTaxPaid { get; set; }
    }
}