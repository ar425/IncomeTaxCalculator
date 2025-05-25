using System.Text.Json;
using IncomeTaxApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxApi.Data.Seed
{
    internal static class DataSeeder
    {
        internal static void SeedData(ModelBuilder modelBuilder)
        {
            SeedTaxBands(modelBuilder);
        }

        private static void SeedTaxBands(ModelBuilder modelBuilder)
        {
            // The tax band values have been put in a separate json file so that they can
            // be changed without needing to re-release the code
            var currentDir = Directory.GetCurrentDirectory();
            var jsonFilePath = Path.Combine(currentDir, "src\\Data", "config.json");

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Could not find config file at: {jsonFilePath}");
            }
            
            // Read and deserialize the JSON file
            var jsonContent = File.ReadAllText(jsonFilePath);
            
            var configData = JsonSerializer.Deserialize<ConfigData>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (configData?.TaxBands != null)
            {
                modelBuilder.Entity<TaxBand>().HasData(configData.TaxBands.ToArray());
            }
        }
    }
}