using FlowerShop.Domain.Interfaces;
using FlowerShop.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FlowerShop.Application.Features.Flowers.Queries;
using FlowerShop.Application.Dtos;
namespace FlowerShop.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<SqlDbOptions> sqlOptions)
        {
            // Add DbContext
            services.Configure(sqlOptions);
            services.AddDbContext<FlowerShopDbContext>((services, options) =>
            {
                var sqlServerOptions = services.GetRequiredService<IOptions<SqlDbOptions>>();
                options.UseSqlServer(sqlServerOptions.Value.ConnectionString
                    //, optionBuilder => optionBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    );

                if (sqlServerOptions.Value.EnableDetailedErrors)
                {
                    options.LogTo(Console.WriteLine, LogLevel.Information);
                    options.EnableDetailedErrors();
                }
            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);


            // Register repositories
            services.AddScoped<IFlowerResponsitory, FlowerRespository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddTransient<IFlowerGetAllHandler<int, IEnumerable<FlowerResponseItem>>, FlowerGetAll>();
            return services;
        }
    }
}
