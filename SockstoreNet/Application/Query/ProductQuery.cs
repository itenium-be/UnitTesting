using Application.Ports;
using Vocabulary;

namespace Application.Query;

public interface IProductQuery
{
    Task<Product?> FindById(ProductId id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> FindAll(CancellationToken cancellationToken);
}

public class ProductQuery(IProductPort productPort) : IProductQuery
{
    public async Task<Product?> FindById(ProductId id, CancellationToken cancellationToken)
    {
        var product = await productPort.FindById(id, cancellationToken);
        return product?.ToProduct();
    }

    public async Task<IEnumerable<Product>> FindAll(CancellationToken cancellationToken)
    {
        var products = await productPort.FindAll(cancellationToken);
        return products.Select(aggregate => aggregate.ToProduct());
    }
}
