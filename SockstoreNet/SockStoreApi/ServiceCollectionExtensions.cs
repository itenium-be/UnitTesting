using Application.Interfaces;
using Application.Ports;
using Application.Query;
using Application.UseCases;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace SockStoreApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register use cases and interfaces
        services.AddScoped<ICreateProduct, CreateProductUseCase>();
        services.AddScoped<IUpdateProduct, UpdateProductUseCase>();
        services.AddScoped<IUpdateStock, UpdateStockUseCase>();
        services.AddScoped<IProductQuery, ProductQuery>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register EF Core DbContext and repository
        services.AddDbContext<ProductDbContext>(options =>
            options.UseInMemoryDatabase("SockStore"));

        services.AddScoped<IProductPort, ProductRepository>();
        return services;
    }
}
