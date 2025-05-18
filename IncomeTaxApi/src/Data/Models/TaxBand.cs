namespace IncomeTaxApi.Data.Models
{
    public interface ITaxBand
    {
         int Id { get; set; }
         int LowerLimit { get; set; }
         int? UpperLimit { get; set; }
         int Rate { get; set; }
    }

    public class TaxBand : ITaxBand
    {
        public int Id { get; set; }
        public int LowerLimit { get; set; }
        public int? UpperLimit { get; set; }
        public int Rate { get; set; }
    }
}