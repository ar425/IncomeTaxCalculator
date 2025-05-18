using IncomeTaxApi.Api.Services;
using IncomeTaxApi.Data;
using IncomeTaxApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IncomeTaxApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => { options.LowercaseUrls = true; });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers();
            
            services.AddTransient<ICalculateIncomeTaxService, CalculateIncomeTaxService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<IIncomeTaxCalculatorContext, IncomeTaxCalculatorContext>(x =>
            {
                x.UseNpgsql(dataSource, b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders();
            app.UseCors();
            app.UseRouting();
        }
    }
}
