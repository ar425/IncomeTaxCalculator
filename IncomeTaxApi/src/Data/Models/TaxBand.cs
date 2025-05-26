namespace IncomeTaxApi.Data.Models
{
    public class TaxBand
    {
        public int Id { get; set; }
        public int LowerLimit { get; set; }
        public int? UpperLimit { get; set; }
        public int Rate { get; set; }
    }
}