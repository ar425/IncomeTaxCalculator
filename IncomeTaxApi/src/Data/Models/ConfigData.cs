namespace IncomeTaxApi.Data.Models;

internal class ConfigData
{
    // A separate class from retreiving specific data from config.json
    public ConfigData(List<TaxBand> taxBands)
    {
        TaxBands = taxBands;
    }

    public List<TaxBand> TaxBands { get; set; }
}