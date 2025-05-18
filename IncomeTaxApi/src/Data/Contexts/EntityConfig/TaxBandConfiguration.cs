using IncomeTaxApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeTaxApi.Data.Contexts.EntityConfig
{
    public class TaxBandConfiguration : IEntityTypeConfiguration<TaxBand>
    {
        public void Configure(EntityTypeBuilder<TaxBand> builder)
        {
            builder.Property(b => b.Id).UseHiLo($"Sequence-{nameof(TaxBand)}");
        }
    }
}