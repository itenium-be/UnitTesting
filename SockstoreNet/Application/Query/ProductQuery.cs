using Application.Ports;
using Vocabulary;

namespace Application.Query;

public interface IProductQuery
{
    Task<Product?> FindById(ProductId id);
    Task<IEnumerable<Product>> FindAll();
}

public class ProductQuery(IProductPort productPort) : IProductQuery
{
    public async Task<Product?> FindById(ProductId id)
    {
        var product = await productPort.FindById(id);
        return product?.ToProduct();
    }

    public async Task<IEnumerable<Product>> FindAll()
    {
        var products = await productPort.FindAll();
        return products.Select(aggregate => aggregate.ToProduct());
    }
}
