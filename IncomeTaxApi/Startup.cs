using System.Reflection;
using IncomeTaxApi.Abstractions;
using IncomeTaxApi.Api.Commands.CalculateIncomeTax;
using IncomeTaxApi.Api.Controllers;
using IncomeTaxApi.Api.Services;
using IncomeTaxApi.Data;
using IncomeTaxApi.Data.Contexts;
using IncomeTaxApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IncomeTax API", Version = "v1" });
            });

            services.AddControllers()
                .AddApplicationPart(typeof(TaxController).Assembly);
            
            // transient; new instance created every time the service is requested
            services.AddTransient<ICalculateIncomeTaxService, CalculateIncomeTaxService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // scoped; single instance per HTTP request or scope
            services.AddScoped<CalculateIncomeTaxCommandValidator>();
            
            // singleton is not used in this application currently, but could be used for something like a memory cache
            // as it would only have one instance for the entire application lifetime
            
            // Registering services in the DI container by scanning the current assembly
            var executingAssembly = Assembly.GetExecutingAssembly();
            
            // This finds all classes implementing these interfaces and registers them
            // dynamically, reducing boilerplate
            services.Scan(scan => scan
                .FromAssemblies(executingAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(c => c.AssignableTo(typeof(IConverter<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableToAny(typeof(IRepositoryBase<>)), false)
                .AsImplementedInterfaces()
                .WithTransientLifetime());
                

            // The default localhost connection is in appsettings.Development.json as leaving it in
            // appsettings.json could lead this to being put into production
            // I wanted to show usage of a real database, but I could have also used an in memory database
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            var dataSource = dataSourceBuilder.Build();

            // Migration files have been added to indicate that migrations have been started and
            // the script was created so that it can be easily used for updates in the future
            // which would create new migration files so that the business can keep track of previously done work
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IncomeTax API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
