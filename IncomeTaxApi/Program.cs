using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IncomeTaxApi
{

    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {

                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // Registering Database
                builder.Services.AddDbContext<IncomeDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

                var app = builder.Build();

                // Configuring the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occurred.");
                throw;
            }
        }
    }
}
