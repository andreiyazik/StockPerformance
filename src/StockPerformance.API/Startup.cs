using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StockPerformance.ExternalServices.AlphaVantage;
using StockPerformance.ExternalServices.Contracts;
using StockPerformance.Infrastructure.Configuration;
using StockPerformance.Persistence.Contracts.Repositories;
using StockPerformance.Persistence.SQLServer;
using StockPerformance.Persistence.SQLServer.Repositories;

namespace StockPerformance.API
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddDbContext<DataContext>( options =>
                     options.UseSqlServer(
                         Configuration.GetConnectionString( "DefaultConnection" ),
                         b => b.MigrationsAssembly( typeof( DataContext ).Assembly.FullName ) ) );

            services.AddCors( o => o.AddPolicy( "CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            } ) );

            services.AddMediatR( typeof( Startup ).GetTypeInfo().Assembly );
            services.AddControllers();

            services.AddTransient<IStockService, AlphaVantageStockService>();
            services.AddTransient<ICandleRepository, CandleRepository>();

            services.Configure<YahooFinanceSettings>( Configuration.GetSection( nameof( YahooFinanceSettings ) ) );
            services.Configure<AlphaVantageSettings>( Configuration.GetSection( nameof( AlphaVantageSettings ) ) );

            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc( "v1", new OpenApiInfo { Title = "StockPerformance API", Version = "v1" } );
            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint( "/swagger/v1/swagger.json", "StockPerformance API" );
            } );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors( "CorsPolicy" );

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
    }
}
