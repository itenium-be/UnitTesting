using Application.Domain;
using Application.Ports;
using Microsoft.EntityFrameworkCore;
using Vocabulary;
using Product = Infrastructure.Entities.Product;

namespace Infrastructure.Db;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}

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

    public async Task<ProductAggregate?> FindById(ProductId id, CancellationToken cancellationToken)
    {
        var entity = await db.Products.FindAsync([id.Value], cancellationToken);
        return entity is null ? null : ToAggregate(entity);
    }

    public async Task<IEnumerable<ProductAggregate>> FindAll(CancellationToken cancellationToken)
    {
        var products = await db.Products.ToListAsync(cancellationToken: cancellationToken);
        return products.Select(ToAggregate);
    }

    private static ProductAggregate ToAggregate(Product e)
    {
        return new(new ProductId(e.Id), new Name(e.Name), new Category(e.Category), new Price(e.Price), new Stock(e.Stock));
    }

    private static Product ToEntity(ProductAggregate a) => new()
    {
        Id = a.Id.Value,
        Name = a.Name.Value,
        Category = a.Category.Value,
        Price = a.Price.Value,
        Stock = a.Stock.Value
    };
}