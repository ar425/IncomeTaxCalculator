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
            var taxBands = new List<TaxBand>
            {
                new() { Id = 1, LowerLimit = 0, UpperLimit = 5000, Rate = 0 },
                new() { Id = 2, LowerLimit = 5000, UpperLimit = 20000, Rate = 20 },
                new() { Id = 3, LowerLimit = 20000, UpperLimit = null, Rate = 40 }
            }.ToArray<object>();
            
            modelBuilder.Entity<TaxBand>()
                .HasData(taxBands);
        }
    }
}