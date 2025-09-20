using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
}