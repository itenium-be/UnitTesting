using Application.Domain;

namespace Infrastructure.Repositories;

public interface IProductRepository
{
    ProductAggregate? FindById(string id);
    IEnumerable<ProductAggregate> FindAll();
    void Save(ProductAggregate product);
}