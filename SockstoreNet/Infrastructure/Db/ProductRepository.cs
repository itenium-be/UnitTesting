using Application.Domain;
using Application.Ports;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Vocabulary;

namespace Infrastructure.Db;

public class ProductRepository(ProductDbContext db) : IProductPort
{
    public async Task Save(ProductAggregate product)
    {
        var entity = ToEntity(product);
        await db.Products.AddAsync(entity);
        await db.SaveChangesAsync();
    }

    public async Task Update(ProductAggregate product)
    {
        var entity = ToEntity(product);
        db.Products.Update(entity);
        await db.SaveChangesAsync();
    }

    public async Task<decimal> GetGlobalDiscount()
    {
        var discount = await db.Parameters.FirstOrDefaultAsync(x => x.Key == "GlobalDiscount");
        if (discount == null)
            return 0;

        if (decimal.TryParse(discount.Value, out decimal result))
            return result;

        return 0;
    }

    public async Task<ProductAggregate?> FindById(ProductId id, CancellationToken cancellationToken)
    {
        var entity = await db.Products.FindAsync([id.Value], cancellationToken);
        return entity is null ? null : await ToAggregate(entity);
    }

    public async Task<IEnumerable<ProductAggregate>> FindAll(CancellationToken cancellationToken)
    {
        var products = await db.Products.ToListAsync(cancellationToken: cancellationToken);
        var result = products.Select(async prod => await ToAggregate(prod));
        return await Task.WhenAll(result);
    }

    private async Task<ProductAggregate> ToAggregate(ProductEntity e)
    {
        decimal globalDiscount = await GetGlobalDiscount();
        return new(
            new ProductId(e.Id),
            new Name(e.Name),
            new Category(e.Category),
            new Price(e.Price, globalDiscount),
            new Stock(e.Stock)
        );
    }

    private static ProductEntity ToEntity(ProductAggregate a) => new()
    {
        Id = a.Id.Value,
        Name = a.Name.Value,
        Category = a.Category.Value,
        Price = a.Price.Value,
        Stock = a.Stock.Value
    };
}