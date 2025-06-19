using Application.Domain;
using Application.Ports;
using Microsoft.EntityFrameworkCore;
using Vocabulary;

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

    public async Task<ProductAggregate?> FindById(ProductId id)
    {
        var entity = await db.Products.FindAsync(id.Value);
        return entity is null ? null : ToAggregate(entity);
    }

    public async Task<IEnumerable<ProductAggregate>> FindAll()
    {
        var products = await db.Products.ToListAsync();
        return products.Select(ToAggregate);
    }

    private static ProductAggregate ToAggregate(Product e) =>
        new(new ProductId(e.Id), new Naam(e.Name), new Categorie(e.Category), new Prijs(e.Price), new Voorraad(e.Stock));

    private static Product ToEntity(ProductAggregate a) => new()
    {
        Id = a.Id.Value,
        Name = a.Naam.Value,
        Category = a.Categorie.Value,
        Price = a.Prijs.Value,
        Stock = a.Voorraad.Value
    };
}