using Application.Domain;
using Application.Ports;
using Microsoft.EntityFrameworkCore;
using Vocabulary;

namespace Infrastructure.Db;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options){
    public DbSet<Product> Products => Set<Product>();
}

public class ProductRepository(ProductDbContext db) : IProductPort {
    public void Save(ProductAggregate product)
    {
        var entity = ToEntity(product);
        db.Products.Add(entity);
        db.SaveChanges();
    }

    public void Update(ProductAggregate product)
    {
        var entity = ToEntity(product);
        db.Products.Update(entity);
        db.SaveChanges();
    }

    public ProductAggregate? FindById(ProductId id)
    {
        var entity = db.Products.Find(id.Value);
        return entity is null ? null : ToAggregate(entity);
    }

    public IEnumerable<ProductAggregate> FindAll()
    {
        return db.Products.Select(ToAggregate).ToList();
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