﻿using System.Reflection;
using IncomeTaxApi.Data.Models;
using IncomeTaxApi.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxApi.Data.Contexts
{
    // For scalability; any future entities created can be put in here, rather than having a separate context for each entity
    public interface IIncomeTaxCalculatorContext : IDbContext
    {
        DbSet<TaxBand> TaxBands { get; set; }
    }

    public class IncomeTaxCalculatorContext : DbContext, IIncomeTaxCalculatorContext
    {
        private readonly ILogger<IncomeTaxCalculatorContext> _logger;
        public IncomeTaxCalculatorContext(DbContextOptions<IncomeTaxCalculatorContext> options,
                                          ILogger<IncomeTaxCalculatorContext> logger) : base(options)
        {
            _logger = logger;
        }

        public DbSet<TaxBand> TaxBands { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

                DataSeeder.SeedData(modelBuilder);

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown when attempting to create model: {E}", ex);
            }
        }
    }
}